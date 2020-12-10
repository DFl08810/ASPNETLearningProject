using DataCore.Entities;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Account> MatchByRelated(int Id, string paramString)
        {
            var option = paramString;
            throw new NotImplementedException();
        }

        public IEnumerable<Account> MatchByString(string matchString)
        {
            //Using simple linq query with OR logic statement to find all matching elements
            var queryResult = _db.Accounts.Where(item => item.Name.Contains(matchString) || item.Email.Contains(matchString));
            return queryResult;
        }

        public bool RemoveRange(IEnumerable<Account> obj)
        {
            _db.RemoveRange(obj);
            return true;
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
            //_db.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            var queryResult = _db.Accounts.AsNoTracking();
            return queryResult.ToList();
        }

        public Account SelectById(int id)
        {
            var result = _db.Accounts.Find(id);
            return result;
        }

        public bool Update(Account obj)
        {
            try
            {
                //Check if entity is being tracked
                var tracking = _db.ChangeTracker.Entries<Account>().Any(x => x.Entity.Id == obj.Id);
                if (!tracking)
                {
                    _db.Accounts.Update(obj);
                }
                else
                {
                    //Get tracked entity with matching ID
                    var trackedEntities = _db.ChangeTracker.Entries<Account>().Where(x => x.Entity.Id == obj.Id);
                    //Detach all tracked entities from tracking
                    foreach (var trackedEntity in trackedEntities)
                    {
                        trackedEntity.State = EntityState.Detached;
                    }
                    //Attach modified entity
                    _db.Accounts.Attach(obj);
                    //set as modified
                    _db.Entry(obj).State = EntityState.Modified;
                    //Update database
                    _db.Accounts.Update(obj);

                }
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public bool UpdateRange(IEnumerable<Account> obj)
        {
            //_db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                _db.Accounts.AttachRange(obj);
                _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
