using IdentityLib.Models;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    public interface IAccountModelService
    {
        IEnumerable<AccountModel> Synchronize(List<User> usersList);
        IEnumerable<AccountModel> GetAllAccounts();
        AccountModel GetAccount(int Id);
        bool DeleteAccount(int Id);
        IEnumerable<AccountModel> SortAllAccounts(string sortMode);
        IEnumerable<AccountModel> GetMatchingAccounts(string matchQuery);
    }
}
