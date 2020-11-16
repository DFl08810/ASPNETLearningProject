using CommandCore.Prefabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.Factories
{
    public interface IArticleModelFactory
    {
        ArticleModel GetArticleModel(ArticlePrefab articlePrefabs);
        List<ArticleModel> GetArticleModels(List<ArticlePrefab> articlePrefabs);
    }
}
