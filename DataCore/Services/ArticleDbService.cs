using DataCore.DataAccess;
using DataCore.Entities;
using System;
using System.Collections.Generic;

namespace DataCore
{
    public class ArticleDbService: IArticleDB
    {
        private readonly IArticleDataAccess _articleAccess;

        public ArticleDbService(IArticleDataAccess articleAccess)
        {
            this._articleAccess = articleAccess;
        }

        public List<Article> GetArticle()
        {
            var queryResult = _articleAccess.SelectAll();
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
