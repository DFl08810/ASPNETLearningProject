using CommandCore.Prefabs;
using DataCore;
using DataCore.DataAccess;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Factories
{
    public class ArticleFactory : IArticleFactory
    {
        private readonly IDataAccess<Article> _dataAccess;

        public ArticleFactory(IDataAccess<Article> dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        //Select all entries in db
        public IEnumerable<Article> GetAll()
        {
            var articles = _dataAccess.SelectAll();
            
            return articles;
        }

        //Select by obj Id
        public Article GetArticle(int Id)
        {
            var article = _dataAccess.SelectById(Id);

            return article;
        }

    }
}
