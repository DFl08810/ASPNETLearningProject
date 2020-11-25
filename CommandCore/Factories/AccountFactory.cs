using DataCore.DataAccess;
using DataCore.Entities;
using IdentityLib.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Factories
{
    public class AccountFactory : IAccountFactory
    {
        private readonly IDataAccess<Account> _dataAccess;

        public AccountFactory(IDataAccess<Account> dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public List<Account> ConstructAccounts(List<User> users)
        {
            var accountModels = new List<Account>();
            foreach (var user in users)
            {
                var account = new Account
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Status = user.IsEnabled
                };
                accountModels.Add(account);
            }

            return accountModels;
        }

        public List<Account> ConstructFromDb()
        {
            var accounts = _dataAccess.SelectAll();

            return accounts.ToList();
        }

        public List<Account> ConstructMatching(string matchString)
        {
            var matchedAccounts = _dataAccess.MatchByString(matchString);
            return matchedAccounts.ToList();
        }
    }
}
