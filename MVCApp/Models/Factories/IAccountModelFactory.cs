using CommandCore.Prefabs;
using DataCore.Entities;
using IdentityLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.Factories
{
    public interface IAccountModelFactory
    {
        AccountModel GetAccountModel(Account accountEnt);
        IEnumerable<AccountModel> GetAccountModels(IEnumerable<Account> accountEnts);
        List<AccountModel> GetAccountModels(List<User> users, bool isNew = false);
    }
}
