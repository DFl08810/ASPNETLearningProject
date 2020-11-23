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
            var queryResult = _db.Accounts;
            return queryResult.ToList();
        }

        public Account SelectById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
