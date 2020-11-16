using CommandCore.Prefabs;
using DataCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Factories
{
    public class ArticlePrefactory : IArticlePrefactory
    {
        private readonly IArticleDB _articleDB;

        public ArticlePrefactory(IArticleDB articleDB)
        {
            this._articleDB = articleDB;
        }

        public List<ArticlePrefab> CreateArticles()
        {
            var articles = _articleDB.GetArticle();
            var articlePrefabs = new List<ArticlePrefab>();

            foreach (var article in articles)
            {
                var articlePrefab = new ArticlePrefab(article);
                articlePrefabs.Add(articlePrefab.ConvertObject());
            }

            return articlePrefabs;
        }

        public List<ArticlePrefab> CreateArticle()
        {
            var articles = _articleDB.GetArticle();

            var single = articles.First();

            ArticlePrefab y = new ArticlePrefab(single);
            y.ConvertObject();


            return new List<ArticlePrefab>{ y };
        }

    }
}
