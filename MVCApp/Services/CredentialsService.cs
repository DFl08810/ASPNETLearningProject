using IdentityLib.Models;
using Microsoft.AspNetCore.Identity;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    public class CredentialsService : ICredentialsService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public CredentialsService(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        public async void CreateDefaultCredentials()
        {
            #region makeAdmin
            var defaultAdmin = new User();
            defaultAdmin.UserName = "admin";
            defaultAdmin.Email = "admin@site.com";
            defaultAdmin.EmailConfirmed = true;
            string adminDefaultPassword = "Admin123*";

            var defaultUser = new User();
            defaultUser.UserName = "usertest";
            defaultUser.Email = "usertest@site.com";
            defaultUser.EmailConfirmed = true;
            string userDefaultPassword = "Usertest123*";


            await UserCreate(defaultAdmin, adminDefaultPassword, RoleDef.Admin);
            await UserCreate(defaultUser, userDefaultPassword, RoleDef.User);
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

        public async Task<bool> ProcessLogin(AccountInputModel input)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, input.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //Uncomment this to enabdle logger, logger provider is not implemented yet
                //_logger.LogInformation("User logged in.");

                //returns true if login is success
                return true;
            }
            else
            {
                //return false if failed login
                return false;
            }
        }
    }
}
