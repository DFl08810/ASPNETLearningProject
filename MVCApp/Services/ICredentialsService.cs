using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    public interface ICredentialsService
    {
        public void CreateRoles();
        public void CreateDefaultCredentials();
    }
}
