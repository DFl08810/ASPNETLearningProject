using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.EntityServices
{
    public interface IArticleService
    {
        List<ArticleModel> FetchArticles();
    }
}
