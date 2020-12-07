using CommandCore.Prefabs;
using DataCore.Entities;
using IdentityLib.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.Factories
{
    //Model factories are used for converting data objects to models used in views
    public class AccountModelFactory : IAccountModelFactory
    {
        private readonly UserManager<User> _userMan;

        public AccountModelFactory(UserManager<User> userManager)
        {
            this._userMan = userManager;
        }

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
                Status = accountEnt.Status,
                IsPending = accountEnt.IsPending
            };
        }

        public IEnumerable<AccountModel> GetAccountModels(IEnumerable<Account> accountEnts)
        {
            List<AccountModel> accounts = new List<AccountModel>();
            foreach (var account in accountEnts)
            {
                AccountModel accountModel = GetAccountModel(account);
                accounts.Add(accountModel);
            }
            return accounts;
        }

        //overloaded accounts creation method, accepts user from identity framework
        public List<AccountModel> GetAccountModels(List<User> users, bool isNew = false)
        {
            var accountModels = new List<AccountModel>();
            foreach(var user in users)
            {
                var account = new AccountModel {
                    Email = user.Email,
                    Name = user.UserName,
                    Status = user.IsEnabled,
                    Role = _userMan.GetRolesAsync(user).Result.FirstOrDefault(),
                    IsPending = false
                };
                //if user is new set pending acceptation request
                if (isNew)
                {
                    account.IsPending = true;
                }
                accountModels.Add(account);
            }

            return accountModels;
        }
    }
}
