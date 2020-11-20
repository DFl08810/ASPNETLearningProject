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
        Task<bool> MakeRegisterRequest(RegistrationModel registration);
    }
}
