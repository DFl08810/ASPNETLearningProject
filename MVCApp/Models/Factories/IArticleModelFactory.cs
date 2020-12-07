using CommandCore.Prefabs;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models.Factories
{
    public interface IArticleModelFactory
    {
        ArticleModel GetArticleModel(Article article);
        IEnumerable<ArticleModel> GetArticleModels(IEnumerable<Article> articles);
    }
}
