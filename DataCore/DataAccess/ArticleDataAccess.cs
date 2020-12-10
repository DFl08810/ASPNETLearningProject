using DataCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.DataAccess
{
    public class ArticleDataAccess : IDataAccess<Article>
    {
        private DataContext _db;
        private readonly ILogger<Exception> _logger;

        public ArticleDataAccess(DataContext db, ILogger<Exception> logger)
        {
            this._db = db;
            this._logger = logger;
        }

        public Article Read(int id)
        {
            return _db.Articles.Find(id);
        }

        public bool Save(Article article)
        {
            _db.Add(article);
            return true;
        }

        public bool SaveRange(IEnumerable<Article> articles)
        {
            _db.AddRange(articles);
            return true;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

        public Article SelectById(int id)
        {
            return _db.Articles.Find(id);
        }

        public IEnumerable<Article> SelectAll()
        {
            var queryResult = _db.Articles;
            return queryResult.ToList();
        }

        public IEnumerable<Article> MatchByString(string matchString)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRange(IEnumerable<Article> obj)
        {
            try
            {
                _db.UpdateRange(obj);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return false;
            }

            return true;
        }

        public bool RemoveRange(IEnumerable<Article> obj)
        {
            _db.RemoveRange(obj);
            return true;
        }

        public IEnumerable<Article> MatchByRelated(int Id, string paramString)
        {
            var option = paramString;
            var queryResult = _db.Articles.Where(item => item.Author.Id.Equals(Id));
            return queryResult;
        }

        public bool Update(Article obj)
        {
            throw new NotImplementedException();
        }
    }
}
