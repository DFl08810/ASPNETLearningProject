using CommandCore.Factories;
using CommandCore.Prefabs;
using CommandCore.Services;
using DataCore.Entities;
using MVCApp.Models;
using MVCApp.Models.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.EntityServices
{
    //Model services are used to get and send data from view layer to data and logic layer

    //This service provides all essential functions for operating with article models
    //It can use factories to fetch articles from database and use the same factories to create objects for writing objects into database
    //It contains auxiliary functions related to sorting lists retrieved from database
    public class ArticleModelService : IArticleModelService
    {
        private readonly IArticleFactory _articleFactory;
        private readonly IArticleModelFactory _articleModelFactory;
        private readonly IArticleService _articleService;

        public ArticleModelService(IArticleFactory articleFactory, IArticleModelFactory articleModelFactory,
                            IArticleService articleService)
        {
            this._articleFactory = articleFactory;
            this._articleModelFactory = articleModelFactory;
            this._articleService = articleService;
        }

        public IEnumerable<ArticleModel> GetAllArticles()
        {
            var articles = _articleFactory.GetAll();

            var articleModels = _articleModelFactory.GetArticleModels(articles);

            return articleModels;
        }

        #region submissionProcess
        public bool PostArticle(ArticleModel articleModel)
        {
            var article = ConvertModelToEntity(articleModel);
            _articleService.SaveRange(new List<Article> { article });
            return true;

        }

        private Article ConvertModelToEntity(ArticleModel articleModel)
        {
            return new Article
            {
                Id = articleModel.Id,
                Title = articleModel.Title,
                Synopsis = articleModel.Synopsis,
                Content = articleModel.Content,
            };
        }
        private IEnumerable<Article> ConvertModelToEntity(List<ArticleModel> articleModels)
        {
            List<Article> articles = new List<Article>();
            foreach (var article in articleModels)
            {
                Article articleEntity = ConvertModelToEntity(article);
                articles.Add(articleEntity);
            }
            return articles;
        }
        #endregion
    }
}
