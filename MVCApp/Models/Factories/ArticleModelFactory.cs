using CommandCore.Prefabs;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.Factories
{
    //Model factories are used for converting data objects to models used in views
    public class ArticleModelFactory : IArticleModelFactory
    {


        public ArticleModel GetArticleModel(Article article)
        {
            return new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                Synopsis = article.Synopsis,
                Content = article.Content
            };
        }

        public IEnumerable<ArticleModel> GetArticleModels(IEnumerable<Article> articles)
        {
            List<ArticleModel> articleModels = new List<ArticleModel>();
            foreach (var article in articles)
            {
                ArticleModel articleModel = GetArticleModel(article);
                articleModels.Add(articleModel);
            }
            return articleModels;
        }
    }
}
