using DataCore.DataAccess;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Services
{
    public class AccountService
    {
        private readonly IDataAccess<Account> _accountDbAccess;

        public AccountService(IDataAccess<Account> accountDbAccess)
        {
            this._accountDbAccess = accountDbAccess;
        }

        public bool Synchronize(IEnumerable<Account> accounts)
        {

            return true;
        }
    }
}
