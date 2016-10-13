using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces for security
using Microsoft.AspNet.Identity;   //RoleManager
using Microsoft.AspNet.Identity.EntityFramework; //IdentityRole, RoleStore
#endregion
namespace ChinookSystem.Security
{
    public class RoleManager: RoleManager<IdentityRole>
    {
        public RoleManager():base (new RoleStore<IdentityRole>(new ApplicationDbContext()))
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
        }
    }
}
