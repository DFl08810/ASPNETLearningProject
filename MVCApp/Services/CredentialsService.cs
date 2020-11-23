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

        private async void CreateDefaultCredentials()
        {
            //define defaults
            #region makeAdmin
            var defaultAdmin = new User();
            defaultAdmin.UserName = "admin";
            defaultAdmin.Email = "admin@site.com";
            defaultAdmin.EmailConfirmed = true;
            defaultAdmin.IsEnabled = true;
            string adminDefaultPassword = "Admin123*";

            var defaultUser = new User();
            defaultUser.UserName = "usertest";
            defaultUser.Email = "usertest@site.com";
            defaultUser.EmailConfirmed = true;
            defaultUser.IsEnabled = true;
            string userDefaultPassword = "Usertest123*";


            await UserCreate(defaultAdmin, adminDefaultPassword, RoleDef.Admin);
            await UserCreate(defaultUser, userDefaultPassword, RoleDef.User);
            #endregion
            #region makeTestUser


            #endregion
        }

        private async void CreateDefaultRoles()
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

        public void InitializeDefaults()
        {
            try
            {
                CreateDefaultRoles();
                CreateDefaultCredentials();
            }
            catch(Exception e)
            {
                //TODO
            }
        }

        public async Task<bool> ProcessLogin(LoginModel input)
        {
            //find user in db and check if user is enabled
            var findUser = await _userManager.FindByNameAsync(input.UserName);
            if(!findUser.IsEnabled)
            {
                //return false if user is enabled, login cannot be completed
                return false;
            }
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

        public async Task<bool> MakeRegisterRequest(RegistrationModel registration)
        {
            var newUserRegistration = new User
            {
                UserName = registration.UserName,
                Email = registration.Email,
                IsEnabled = false
            };

            //call user create that returns true if creation is succcessfull
            return await UserCreate(newUserRegistration, registration.Password, RoleDef.User);
        }

        private async Task<bool> UserCreate(User user, string password, string role)
        {
            //userManager is called to create desired default creds
            IdentityResult chkUser = await _userManager.CreateAsync(user, password);

            if (chkUser.Succeeded)
            {
                //assign roles to default
                var result1 = await _userManager.AddToRoleAsync(user, role);
                return true;
            }
            //false state means somethin went wrong
            return false;
        }

        public async Task<List<User>> RetrieveUsers()
        {
            var users = _userManager.Users;
            return users.ToList();
        }
    }
}
