using DataCore.DataAccess;
using DataCore.Entities;
using IdentityLib.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Factories
{
    public class AccountFactory : IAccountFactory
    {
        private readonly IDataAccess<Account> _dataAccess;
        private readonly UserManager<User> _userMan;

        public AccountFactory(IDataAccess<Account> dataAccess, UserManager<User> userManager)
        {
            this._dataAccess = dataAccess;
            this._userMan = userManager;
        }

        public List<Account> ConstructAccounts(List<User> users, bool isNew = false, bool isDefault = false)
        {
            var accountModels = new List<Account>();
            foreach (var user in users)
            {
                var account = new Account
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Status = user.IsEnabled,
                    Registered = DateTime.Now,
                    Role = _userMan.GetRolesAsync(user).Result.FirstOrDefault(),
                    IsPending = false
                };
                //if user is new set pending acceptation request
                if (isNew && !isDefault)
                {
                    account.IsPending = true;
                }
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

        public Account GetAccount(int Id)
        {
            var account = _dataAccess.SelectById(Id);
            return account;

        }
    }
}
