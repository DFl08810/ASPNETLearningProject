using CommandCore.Prefabs;
using DataCore;
using DataCore.DataAccess;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Services
{
    //Service for publishing articles to database
    public class ArticleService : IArticleService
    {
        private readonly IDataAccess<Article> _dataAccess;

        public ArticleService(IDataAccess<Article> dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        public bool DeleteArticle(int Id)
        {
            var articleToDelete = _dataAccess.SelectById(Id);
            _dataAccess.RemoveRange(new List<Article> { articleToDelete });
            _dataAccess.Commit();
            return true;
        }


        public IEnumerable<Article> SaveRange(List<Article> articles)
        {
            _dataAccess.SaveRange(articles);
            _dataAccess.Commit();
            return articles;
        }

    }
}
