using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.AspNet.Identity.EntityFramework;    //UserStore
using Microsoft.AspNet.Identity;                    //UserManager
using System.ComponentModel;                        //ODS
using ChinookSystem.DAL;                            //context class
using ChinookSystem.Data.Entities;                  //entity classes
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
                    Email = string.Format(STR_EMAIL_FORMAT, STR_WEBMASTER_USERNAME)
                };
                //this Create command is from the inherited UserManager class
                //this command creates a record on the security Users table (AspNetUsers)
                this.Create(webMasterAccount, STR_DEFAULT_PASSWORD);
                //this AddToRole command is from the inherited UserManager class
                //this command creates a record on the security UserRole table (AspNetUserRoles)
                this.AddToRole(webMasterAccount.Id, SecurityRoles.WebsiteAdmins);
            }
        }//eom

        //create the CRUD methods for adding a user to the security User table
        //read of data to display on gridview
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UnRegisteredUserProfile> ListAllUnregisteredUsers()
        {
            using (var context = new ChinookContext())
            {
                //the data needs to be in memory for execution by the next query
                //to complish this use .ToList() which will force the query to execute

                //List() set containing the list of employeeids
                var registeredEmployees = (from emp in Users
                                           where emp.EmployeeID.HasValue
                                           select emp.EmployeeID).ToList();
                //compare the List() set to the user data table Employees
                var unregisteredEmployees = (from emp in context.Employees
                                             where !registeredEmployees.Any(eid => emp.EmployeeId == eid)
                                             select new UnRegisteredUserProfile()
                                             {
                                                 CustomerEmployeeId = emp.EmployeeId,
                                                 FirstName = emp.FirstName,
                                                 LastName = emp.LastName,
                                                 UserType = UnRegisteredUserType.Employee
                                             }).ToList();

                //List() set containing the list of customerids
                var registeredCustomers = (from cus in Users
                                           where cus.CustomerID.HasValue
                                           select cus.CustomerID).ToList();
                //compare the List() set to the user data table Customers
                var unregisteredCustomers = (from cus in context.Customers
                                             where !registeredCustomers.Any(cid => cus.CustomerId == cid)
                                             select new UnRegisteredUserProfile()
                                             {
                                                 CustomerEmployeeId = cus.CustomerId,
                                                 FirstName = cus.FirstName,
                                                 LastName = cus.LastName,
                                                 UserType = UnRegisteredUserType.Customer
                                             }).ToList();
                //combine the two physically identical layout datasets
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
                UserName = userinfo.AssignedUserName,
                Email = userinfo.AssignedEmail
            };

            //set the CustomerId or EmployeeId
            switch (userinfo.UserType)
            {
                case UnRegisteredUserType.Customer:
                    {
                        newuseraccount.CustomerID = userinfo.CustomerEmployeeId;

                        break;
                    }
                case UnRegisteredUserType.Employee:
                    {
                        newuseraccount.EmployeeID = userinfo.CustomerEmployeeId;
                        break;
                    }
            }

            //create the actual AspNetUser record
            this.Create(newuseraccount, STR_DEFAULT_PASSWORD);

            //assign user to an appropriate role
            //uses the guid like user Id from the User's table
            switch (userinfo.UserType)
            {
                case UnRegisteredUserType.Customer:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.RegisteredUsers);
                        break;
                    }
                case UnRegisteredUserType.Employee:
                    {
                        this.AddToRole(newuseraccount.Id, SecurityRoles.Staff);
                        break;
                    }
            }

        }//eom

        //list all current users
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UserProfile> ListAllUsers()
        {
            //we will be using the RoleManager to get roles
            var rm = new RoleManager();

            //get the current users off the User security table
            var results = from person in Users.ToList()
                          select new UserProfile()
                          {
                              UserID = person.Id,
                              UserName = person.UserName,
                              Email = person.Email,
                              EmailConfirmed = person.EmailConfirmed,
                              CustomerId = person.CustomerID,
                              EmployeeId = person.EmployeeID,
                              RoleMemberships = person.Roles.Select(r => rm.FindById(r.RoleId).Name)
                          };

            //using our own data tables,gather the user FirstName and LastName
            using (var context = new ChinookContext())
            {
                Employee etemp;
                Customer ctemp;
                foreach (var person in results)
                {
                    if (person.EmployeeId.HasValue)
                    {
                        etemp = context.Employees.Find(person.EmployeeId);
                        person.FirstName = etemp.FirstName;
                        person.LastName = etemp.LastName;
                    }
                    else if (person.CustomerId.HasValue)
                    {
                        ctemp = context.Customers.Find(person.CustomerId);
                        person.FirstName = ctemp.FirstName;
                        person.LastName = ctemp.LastName;
                    }
                    else
                    {
                        person.FirstName = "unknown";
                        person.LastName = "";
                    }
                }
            }
            return results.ToList();
        }//eom

        //add a user to the User Table (ListView)
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddUser(UserProfile userinfo)
        {
            //create an instance representing the new user
            var useraccount = new ApplicationUser()
            {
                UserName = userinfo.UserName,
                Email = userinfo.Email
            };

            //create the new user on the physical Users table
            this.Create(useraccount, STR_DEFAULT_PASSWORD);

            //create the UserRoles which were choosen at insert time
            foreach (var roleName in userinfo.RoleMemberships)
            {
                this.AddToRole(useraccount.Id, roleName);
            }
        }//eom

        //delete a user from the user Table (ListView)
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void RemoveUser(UserProfile userinfo)
        {
            //business rule
            //the webmaster cannot be deleted

            //realize that the only information you have at this time
            //is the DataKeyNames value which is the User ID
            // (on the User security table the field is Id)

            //obtain the username from the security user table using
            //the User ID value

            string UserName = this.Users.Where(u => u.Id == userinfo.UserID)
                            .Select(u => u.UserName).SingleOrDefault().ToString();

            //remove the user
            if (UserName.Equals(STR_WEBMASTER_USERNAME))
            {
                throw new Exception("The webmaster account cannot be removed.");
            }
            this.Delete(this.FindById(userinfo.UserID));
        }
    }//eoc
}//eon
