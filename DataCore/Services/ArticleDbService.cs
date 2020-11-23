using DataCore.DataAccess;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataCore
{
    public class ArticleDbService: IArticleDB
    {
        private readonly IDataAccess<Article> _articleAccess;

        public ArticleDbService(IDataAccess<Article> articleAccess)
        {
            this._articleAccess = articleAccess;
        }

        public List<Article> GetArticle()
        {
            var queryResult = _articleAccess.SelectAll().ToList();
            return queryResult;
        }

        public bool PostArticle(Article article)
        {
            var writingVar = article;
            //TODO: logic for database update
            _articleAccess.Save(article);
            _articleAccess.Commit();

            return true;
        }
    }
}