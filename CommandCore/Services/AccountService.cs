using DataCore.DataAccess;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDataAccess<Account> _accountDbAccess;

        public AccountService(IDataAccess<Account> accountDbAccess)
        {
            this._accountDbAccess = accountDbAccess;
        }


        public IEnumerable<Account> Synchronize(List<Account> accounts)
        {
            //Get current content from account table

            var accountsDbContent = _accountDbAccess.SelectAll();
            //empty database is filled with data from identity
            if(!accountsDbContent.Any())
            {
                _accountDbAccess.SaveRange(accounts);
                _accountDbAccess.Commit();
                return accounts;
            }
            return accounts;
        }
    }
}
