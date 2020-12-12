using IdentityLib.Models;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    public interface ICredentialsService
    {
        Task<bool> ProcessLogin(LoginModel input);
        void InitializeDefaults();
        Task<bool> MakeRegisterRequest(RegistrationModel registration, bool isDefault = false);
        Task<List<User>> RetrieveUsers();
        bool DeleteUser(int Id, System.Security.Claims.ClaimsPrincipal currentUser);
        User GetUser(int accountId);

        bool SetUserStatus(int accountId, bool status);

        bool UpdateUser(User user, string role, System.Security.Claims.ClaimsPrincipal currentUser);
    }
}
