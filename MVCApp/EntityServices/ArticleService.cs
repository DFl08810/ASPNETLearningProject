using CommandCore.Factories;
using MVCApp.Models;
using MVCApp.Models.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.EntityServices
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleFactory _articleFactory;
        private readonly IArticleModelFactory _articleModelFactory;

        public ArticleService(IArticleFactory articleFactory, IArticleModelFactory articleModelFactory)
        {
            this._articleFactory = articleFactory;
            this._articleModelFactory = articleModelFactory;
        }

        public List<ArticleModel> FetchArticles()
        {
            var articlesFab = _articleFactory.CreateArticles();

            var articleModels = _articleModelFactory.GetArticleModels(articlesFab);

            return articleModels;
        }
    }
}
