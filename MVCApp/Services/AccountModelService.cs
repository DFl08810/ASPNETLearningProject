using CommandCore.Factories;
using CommandCore.Services;
using IdentityLib.Models;
using MVCApp.Models;
using MVCApp.Models.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Services
{
    public class AccountModelService : IAccountModelService
    {
        private readonly IAccountFactory _accountFactory;
        private readonly IAccountModelFactory _accountModelFactory;
        private readonly IAccountService _accountService;

        public AccountModelService(IAccountFactory accountFactory, IAccountModelFactory accountModelFactory, IAccountService accountService)
        {
            this._accountFactory = accountFactory;
            this._accountModelFactory = accountModelFactory;
            this._accountService = accountService;
        }

        public bool DeleteAccount(int Id)
        {
            var result = _accountService.DeleteAccount(Id);
            return result;
        }

        public AccountModel GetAccount(int Id)
        {
            var account = _accountFactory.GetAccount(Id);
            var accountModel = _accountModelFactory.GetAccountModel(account);
            return accountModel;
        }

        public IEnumerable<AccountModel> GetAllAccounts()
        {
            var accountEntites = _accountFactory.ConstructFromDb();
            var result = _accountModelFactory.GetAccountModels(accountEntites);
            return result;
        }

        public IEnumerable<AccountModel> GetMatchingAccounts(string matchQuery)
        {
            //find records with matching fields in database
            var resultAccounts = _accountFactory.ConstructMatching(matchQuery);
            var result = _accountModelFactory.GetAccountModels(resultAccounts);
            return result;
        }

        //accounts sorting method
        public IEnumerable<AccountModel> SortAllAccounts(string sortMode)
        {
            //get all entities
            var accountEntites = _accountFactory.ConstructFromDb();


            //order mode selection
            switch (sortMode)
            {
                case "nameDesc":
                    accountEntites = accountEntites.OrderByDescending(obj => obj.Name).ToList();
                    break;
                case "nameAsc":
                    accountEntites = accountEntites.OrderBy(obj => obj.Name).ToList();
                    break;
                case "regDateDesc":
                    accountEntites = accountEntites.OrderByDescending(obj => obj.Registered).ToList();
                    break;
                case "regDateAsc":
                    accountEntites = accountEntites.OrderBy(obj => obj.Registered).ToList();
                    break;
                case "logDateDesc":
                    accountEntites = accountEntites.OrderByDescending(obj => obj.LastLogin).ToList();
                    break;
                case "logDateAsc":
                    accountEntites = accountEntites.OrderBy(obj => obj.LastLogin).ToList();
                    break;
            }
            //convert to view model and return to controller
            var result = _accountModelFactory.GetAccountModels(accountEntites);
            return result;
        }

        public IEnumerable<AccountModel> Synchronize(List<User> usersList)
        {
            //construct account entities from identity userlist
            var acountEntities = _accountFactory.ConstructAccounts(usersList);
            //compare expected account entities with database and conform it
            var accountSynced = _accountService.Synchronize(acountEntities);
            var result = _accountModelFactory.GetAccountModels(accountSynced.ToList());
 
            return result;
        }
    }
}
