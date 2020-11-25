using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.DataAccess
{
    public class AccountDataAccess : IDataAccess<Account>
    {
        private DataContext _db;
        public AccountDataAccess(DataContext db)
        {
            this._db = db;
        }

        public int Commit()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
            return 1;
        }

        public IEnumerable<Account> MatchByString(string matchString)
        {
            //Using simple linq query with OR logic statement to find all matching elements
            var queryResult = _db.Accounts.Where(item => item.Name.Contains(matchString) || item.Email.Contains(matchString));
            return queryResult;
        }

        public bool Save(Account article)
        {
            _db.Add(article);
            return true;
        }

        public bool SaveRange(IEnumerable<Account> accounts)
        {
            _db.AddRange(accounts);
            return true;
        }

        public IEnumerable<Account> SelectAll()
        {
            var queryResult = _db.Accounts.AsNoTracking();
            return queryResult.ToList();
        }

        public Account SelectById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRange(IEnumerable<Account> obj)
        {
            //_db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                _db.UpdateRange(obj);
            }
            catch (Exception exc)
            {
                return false;
            }

            return true;
        }
    }
}
