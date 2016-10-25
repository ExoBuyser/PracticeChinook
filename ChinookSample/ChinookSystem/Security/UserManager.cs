using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity.EntityFramework; //UserStore
using Microsoft.AspNet.Identity; //UserManager
using System.ComponentModel;  //ODS
using ChinookSystem.DAL;    //Context class
#endregion

namespace ChinookSystem.Security
{
    [DataObject]
    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }

        //setting up the default webMaster
        #region Constants
        private const string STR_DEFAULT_PASSWORD = "Pa$$word1";
        private const string STR_USERNAME_FORMAT = "{0}.{1}";
        private const string STR_EMAIL_FORMAT = "{0}@Chinook.ca";
        private const string STR_WEBMASTER_USERNAME = "Webmaster";
        #endregion

        public void AddWebmaster()
        {
            if (!Users.Any(u => u.UserName.Equals(STR_WEBMASTER_USERNAME)))
            {
                var webMasterAccount = new ApplicationUser()
                {
                    UserName = STR_WEBMASTER_USERNAME,
                    Email = string.Format(STR_EMAIL_FORMAT, STR_USERNAME_FORMAT)
                };

                //this Create commandis from the inherited UserManager class
                //this command creates a record on the security Users table(AspNetUsers)
                this.Create(webMasterAccount, STR_DEFAULT_PASSWORD);
                //this AddtoRole command is from the inherited UserManager class
                //this command creates a record on the security UserRole table (AspNetUserRoles)
                this.AddToRole(webMasterAccount.Id, SecurityRoles.WebsiteAdmins);

            }
        }//eom
        //create the CRUD methods for adding to the security User table
        //read of data to display on gridview
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<UnRegisteredUserProfile> ListAllUnregisteredUsers()
        {
            using (var context = new ChinookContext())
            {
                //the data needs to be in memory for execustion by the next query
                //to accomplish this use .ToList() which will force the query to execute
                
                //List() set containing the list of employeeids
                var registeredEmployees = (from emp in Users
                                           where emp.EmployeeID.HasValue
                                           select 

                //IEnumerable set containing the list of employeeids
                var registeredEmployees = from emp in Users
                                          where emp.EmployeeID.HasValue
                                          select emp.EmployeeID;
                //compare the IEnumerable set to the user data table Employees

                var unregisteredEmployees = (from emp in context.Employees
                                            where !registeredEmployees.Any(eid => emp.EmployeeId == eid)
                                            select new UnRegisteredUserProfile()
                                            {
                                                UserId = emp.EmployeeId,
                                                FirstName = emp.FirstName,
                                                LastName = emp.LastName,
                                                UserType = UnRegisteredUserType.Employee
                                            }).ToList();



                //IEnumerable set containing the list of employeeids
                var registeredCustomers = from cus in Users
                                          where cus.CustomerID.HasValue
                                          select cus.CustomerID;
                //compare the IEnumerable set to the user data table Employees

                var unregisteredCustomers = (from cus in context.Customers
                                            where !registeredCustomers.Any(cid => cus.CustomerId == cid)
                                            select new UnRegisteredUserProfile()
                                            {
                                                UserId = cus.CustomerId,
                                                FirstName = cus.FirstName,
                                                LastName = cus.LastName,
                                                UserType = UnRegisteredUserType.Customer
                                            }).ToList();

                //combine ther two physically identical layout datasets

                return unregisteredEmployees.Union(unregisteredCustomers).ToList();
            }
        }//eom


        //register a user to the User Table (gridview)
        public void RegisterUser(UnRegisteredUserProfile userinfo)
        {
            //basic information need for the security user record
            //password, email, username
            //you could randomly generate a password, we will use the default password
            //the instance of the required user is based on our ApplicationUser

            var newuseraccount = new ApplicationUser()
            {
                UserName = userinfo.UserName,
                Email = userinfo.Email
            };

            //set the CustomerId or EmployeeId

            switch (userinfo.UserType)
            {
                case UnRegisteredUserType.Customer:
                    {
                        newuseraccount.Id = userinfo.UserId.ToString();
                        break;
                    }
                case UnRegisteredUserType.Employee:
                    {
                        newuseraccount.Id = userinfo.UserId.ToString();
                        break;
                    }
            }

            //create the actual AspNetUser record

            this.Create(newuseraccount, STR_DEFAULT_PASSWORD);

            //assign user to an appropriate role

            switch (userinfo.UserType)
            {
                case UnRegisteredUserType.Customer:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.RegisteredUser);
                        break;
                    }
                case UnRegisteredUserType.Employee:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.RegisteredUser);
                        break;
                    }
            }
        }//eom

        //add a user to the User Table(ListView)

        //delete a user from the user Table(ListView)

    }//eoc
}//eoc
