using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.EntityServices
{
    public interface IArticleModelService
    {
        IEnumerable<ArticleModel> GetAllArticles();

        bool PostArticle(ArticleModel article, string userName);
    }
}
