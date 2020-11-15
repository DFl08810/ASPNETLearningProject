using CommandCore.Prefabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.Factories
{
    public class ArticleModelFactory : IArticleModelFactory
    {


        public ArticleModel GetArticleModel(ArticlePrefab articlePrefab)
        {
            return new ArticleModel
            {
                Id = articlePrefab.Id,
                Title = articlePrefab.Title,
                Synopsis = articlePrefab.Synopsis,
                Content = articlePrefab.Content
            };
        }

        public List<ArticleModel> GetArticleModels(List<ArticlePrefab> articlePrefabs)
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            foreach (var article in articlePrefabs)
            {
                ArticleModel articleModel = GetArticleModel(article);
                articles.Add(articleModel);
            }
            return articles;
        }
    }
}
