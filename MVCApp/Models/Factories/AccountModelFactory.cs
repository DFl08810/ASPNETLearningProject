using CommandCore.Prefabs;
using DataCore.Entities;
using IdentityLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.Factories
{
    public class AccountModelFactory : IAccountModelFactory
    {
        public AccountModel GetAccountModel(Account accountEnt)
        {
            return new AccountModel
            {
                Id = accountEnt.Id,
                Role = accountEnt.Role,
                Email = accountEnt.Email,
                LastLogin = accountEnt.LastLogin,
                Name = accountEnt.Name,
                NoOfArticles = accountEnt.NoOfArticles,
                NoOfComments = accountEnt.NoOfComments,
                Registered = accountEnt.Registered,
                Status = accountEnt.Status
            };
        }

        public List<AccountModel> GetAccountModels(List<Account> accountEnts)
        {
            List<AccountModel> accounts = new List<AccountModel>();
            foreach (var account in accountEnts)
            {
                AccountModel accountModel = GetAccountModel(account);
                accounts.Add(accountModel);
            }
            return accounts;
        }

        public List<AccountModel> GetAccountModels(List<User> users)
        {
            var accountModels = new List<AccountModel>();
            foreach(var user in users)
            {
                var account = new AccountModel {
                    Email = user.Email,
                    Name = user.UserName,
                    Status = user.IsEnabled,
                };
                accountModels.Add(account);
            }

            return accountModels;
        }
    }
}
