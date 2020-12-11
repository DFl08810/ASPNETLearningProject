using CommandCore.Factories;
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
        private readonly IAccountFactory _accountFactory;
        private readonly IAccountService _accountService;

        public ArticleService(IDataAccess<Article> dataAccess, IAccountFactory accountFactory, IAccountService accountService)
        {
            this._dataAccess = dataAccess;
            this._accountFactory = accountFactory;
            this._accountService = accountService;
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

        public Article SaveNew(Article article)
        {
            article.Author = _accountFactory.ConstructMatching(article.Author.Name).FirstOrDefault();

            article.Author.NoOfArticles = CountAuthorArticles(article.Author) + 1;
            //Update author
            _accountService.UpdateRange(new List<Account> { article.Author });
            //Add article with author to database
            _dataAccess.Save(article);
            _dataAccess.Commit();
            return article;
        }

        private int CountAuthorArticles(Account author)
        {
            var articles = _dataAccess.MatchByRelated(author.Id, "author");
            return articles.Count();
        }

    }
}
