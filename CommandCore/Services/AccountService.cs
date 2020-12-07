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

        public bool DeleteAccount(int Id)
        {

            var accountToDelete = _accountDbAccess.SelectById(Id);
            _accountDbAccess.RemoveRange(new List<Account> { accountToDelete });
            _accountDbAccess.Commit();
            return true;
        }

        public IEnumerable<Account> SaveRange(List<Account> accounts)
        {
            _accountDbAccess.SaveRange(accounts);
            _accountDbAccess.Commit();
            return accounts;
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
            //comparation if identical length
            if (accounts.Count.Equals(accountsDbContent.Count()))
            {
                var difference = accounts.Where(a => !accountsDbContent.Any(b => b.Email == a.Email)).ToList();
                
                var attachableAccounts = MakeAttacheble(accounts, accountsDbContent);
                //compare incoming and existing accounts, if none, return db content
                var diffItems = attachableAccounts.Except(accountsDbContent);
                if(diffItems.Count().Equals(0))
                {
                    return accountsDbContent;
                }
                //yehaw, we can safe all difference
                _accountDbAccess.UpdateRange(diffItems);
                _accountDbAccess.Commit();
            }
            //Add new 
            if(accounts.Count() > accountsDbContent.Count() )
            {
                //set all ids to null, so diff can work on two account sets
                var difference = accounts.Where(a => !accountsDbContent.Any(b => b.Email == a.Email)).ToList();
                _accountDbAccess.SaveRange(difference);
                _accountDbAccess.Commit();
            }
            if(accounts.Count() < accountsDbContent.Count())
            {
                //returns all elements from app db that are not in accounts from identity
                var difference = accountsDbContent.Where(a => !accounts.Any(b => b.Email == a.Email)).ToList();
                _accountDbAccess.RemoveRange(difference);
                _accountDbAccess.Commit();
                var x = 0;
                //something shoulg get here
            }



            //Method that assigns ids to accounts so EF Core can apply modify state
            List<Account>  MakeAttacheble(IEnumerable<Account> foreignAccounts, IEnumerable<Account> attachableAccounts)
            {
                var outList = new List<Account>();
                foreach (var item in attachableAccounts)
                {
                    item.Role = foreignAccounts.Where(i => i.Email == item.Email && i.Name == item.Name)
                        .FirstOrDefault().Role;
                    outList.Add(item);
                }
                return outList;
            }
            return _accountDbAccess.SelectAll(); 
        }
    }
}
