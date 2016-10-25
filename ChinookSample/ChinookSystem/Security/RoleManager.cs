using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces for security
using Microsoft.AspNet.Identity;   //RoleManager
using Microsoft.AspNet.Identity.EntityFramework; //IdentityRole, RoleStore
using System.ComponentModel;                  //ODS
#endregion
namespace ChinookSystem.Security
{
    [DataObject]
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager() : base(new RoleStore<IdentityRole>(new ApplicationDbContext()))
        {

        }

        //this method will be executed when the application starts up under IIS

        public void AddStartupRoles()
        {
            foreach (string roleName in SecurityRoles.StartupSecurityRoles)
            {
                //check if the roles already exists in the security tables located in the database

                if (!Roles.Any(r => r.Name.Equals(roleName)))
                {
                    this.Create(new IdentityRole(roleName));
                }
            }
        }//eom

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RoleProfile> ListAllRoles()
        {
            var um = new UserManager();
            //data from roles needs to be in memory
            //for use by the query --> use .ToList()
            var results = from role in Roles.ToList()
                          select new RoleProfile
                          {
                              RoleId = role.Id, //security table
                              RoleName = role.Name,
                              UserName = role.Users.Select(r => um.FindById(r.UserId).UserName)
                          };
            return results.ToList();
        }//eom

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddRole(RoleProfile role)
        {
            //anu business roles to consider
            //the role should not already exist on the Roles table

            if (!this.RoleExists(role.RoleName))
            {
                this.Create(new IdentityRole(role.RoleName));
            }
        }//eom

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void RemoveRole(RoleProfile role)
        {
            this.Delete(this.FindById(role.RoleId));
        }//eom

        //this method will produce a list of all role names

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<string> ListAllRoleNames()
        {
            return this.Roles.Select(r => r.Name).ToList();
        }
    }//eoc
}//eon
