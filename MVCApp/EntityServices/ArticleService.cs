using CommandCore.Factories;
using CommandCore.Prefabs;
using CommandCore.Services;
using MVCApp.Models;
using MVCApp.Models.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.EntityServices
{

    //This service provides all essential functions for operating with article models
    //It can use factories to fetch articles from database and use the same factories to create objects for writing objects into database
    //It contains auxiliary functions related to sorting lists retrieved from database
    public class ArticleService : IArticleService
    {
        private readonly IArticlePrefactory _articlePrefactory;
        private readonly IArticleModelFactory _articleModelFactory;
        private readonly IPublishingService _publishingService;

        public ArticleService(IArticlePrefactory articlePrefactory, IArticleModelFactory articleModelFactory,
                            IPublishingService publishingService)
        {
            this._articlePrefactory = articlePrefactory;
            this._articleModelFactory = articleModelFactory;
            this._publishingService = publishingService;
        }

        public List<ArticleModel> FetchArticles()
        {
            var articlesFab = _articlePrefactory.CreateArticles();

            var articleModels = _articleModelFactory.GetArticleModels(articlesFab);

            return articleModels;
        }

        #region submissionProcess
        public bool PostArticle(ArticleModel article)
        {
            var articlePrefab = new ArticlePrefab(article.Id, article.Title, article.Synopsis, article.Content);
            var result = _publishingService.PublishData(articlePrefab);

            return result;

        }
        #endregion
    }
}
