using CommandCore.Prefabs;
using DataCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Factories
{
    public class ArticleFactory
    {
        private readonly IArticleDB _articleDB;

        public ArticleFactory(IArticleDB articleDB)
        {
            this._articleDB = articleDB;
        }

        public List<ArticlePrefab> CreateArticles()
        {
            var x = _articleDB.GetArticle();

            var single = x.First();

            ArticlePrefab y = new ArticlePrefab(single);
            y.ConvertObject();


            return new List<ArticlePrefab>{ y };
        }

    }
}
