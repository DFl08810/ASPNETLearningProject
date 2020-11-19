using IdentityLib.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    public class CredentialsService : ICredentialsService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public CredentialsService(UserManager<User> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async void CreateDefaultCredentials()
        {
            #region makeAdmin
            var defaultAdmin = new User();
            defaultAdmin.UserName = "admin";
            defaultAdmin.Email = "admin@site.com";
            defaultAdmin.EmailConfirmed = true;
            string adminDefaultPassword = "admin";

            var defaultUser = new User();
            defaultUser.UserName = "usertest";
            defaultUser.Email = "usertest@site.com";
            defaultUser.EmailConfirmed = true;
            string userDefaultPassword = "usertest";


            await UserCreate(defaultAdmin, adminDefaultPassword, RoleDef.Admin);
            await UserCreate(defaultUser, userDefaultPassword, RoleDef.Admin);
            #endregion
            #region makeTestUser


            #endregion
            async Task UserCreate(User user, string defaultPassword, string role)
            {
                IdentityResult chkUser = await _userManager.CreateAsync(user, defaultPassword);


                if (chkUser.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, role);
                }
            }
        }

        public async void CreateRoles()
        {
            //Retrieves all roles from RoleDef static method
            var enumeratedRoles = RoleDef.EnumRoles();
            //Enumerate over list of roles and create role if  needed
            foreach (var role in enumeratedRoles)
            {
                //Checks if role exists in database
                bool roleExist = await _roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    //assign role to database
                    var newRole = new IdentityRole();
                    newRole.Name = role;
                    await _roleManager.CreateAsync(newRole);
                }
            }
        }
    }
}
