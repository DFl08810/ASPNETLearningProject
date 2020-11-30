using CommandCore.Factories;
using CommandCore.Services;
using IdentityLib.Models;
using Microsoft.AspNetCore.Identity;
using MVCApp.Models;
using MVCApp.Models.Factories;
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
        private readonly IAccountService _accountService;
        private readonly IAccountFactory _accountFactory;

        public CredentialsService(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                IAccountService accountService,
                                IAccountFactory accountFactory)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._accountService = accountService;
            this._accountFactory = accountFactory;
        }

        private async void CreateDefaultCredentials()
        {
            //define defaults
            #region makeAdmin
            var defaultAdmin = new User();
            defaultAdmin.UserName = "Admin";
            defaultAdmin.Email = "admin@site.com";
            defaultAdmin.EmailConfirmed = true;
            defaultAdmin.IsEnabled = true;
            string adminDefaultPassword = "Admin123*";

            var defaultUser = new User();
            defaultUser.UserName = "Usertest";
            defaultUser.Email = "usertest@site.com";
            defaultUser.EmailConfirmed = true;
            defaultUser.IsEnabled = true;
            string userDefaultPassword = "Usertest123*";


            await UserCreate(defaultAdmin, adminDefaultPassword, RoleDef.Admin, true);
            await UserCreate(defaultUser, userDefaultPassword, RoleDef.User, true);
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

        public async Task<bool> MakeRegisterRequest(RegistrationModel registration, bool isDefault = false)
        {
            var newUserRegistration = new User
            {
                UserName = registration.UserName,
                Email = registration.Email,
                IsEnabled = false
            };

            //call user create that returns true if creation is succcessfull
            return await UserCreate(newUserRegistration, registration.Password, RoleDef.User, isDefault);
        }

        private async Task<bool> UserCreate(User user, string password, string role, bool isDefault = false)
        {
            //userManager is called to create desired default creds
            IdentityResult chkUser = await _userManager.CreateAsync(user, password);

            if (chkUser.Succeeded)
            {
                //assign roles to default
                var chkRole = await _userManager.AddToRoleAsync(user, role);
                //check for role assignment
                if (chkRole.Succeeded)
                {
                    //create list from user and pass it to main app database
                    AssingNewUserToAppDb(new List<User> { user }, isDefault);
                    
                }
                return true;
            }
            //false state means somethin went wrong
            return false;
        }

        //create record for new user in local app db separate from identity db
        private bool AssingNewUserToAppDb(List<User> users, bool isDefault = false)
        {
            //set optional isNew parameter to true, so factory can assing pending acceptation flag to account
            var accountModels = _accountFactory.ConstructAccounts(users, true, isDefault);
            _accountService.SaveRange(accountModels);
            return true;
        }

        public async Task<List<User>> RetrieveUsers()
        {
            var users = _userManager.Users;
            return users.ToList();
        }

        public bool DeleteUser(int Id, System.Security.Claims.ClaimsPrincipal currentUser)
        {
            var user = GetUser(Id);
            var role = _userManager.GetRolesAsync(user).Result;



            if(role.FirstOrDefault() == RoleDef.Admin || currentUser.Identity.Name == user.UserName)
            {
                return false;
            }
            else
            {
                var result = _userManager.DeleteAsync(user);

                if (result.Result.Succeeded)
                {
                    return true;
                }
            }

            return false;
            
        }

        public User GetUser(int Id)
        {
            var account = _accountFactory.GetAccount(Id);
            var user = _userManager.FindByNameAsync(account.Name);
            return user.Result;
        }
    }
}
