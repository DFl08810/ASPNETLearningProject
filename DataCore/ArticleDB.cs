using DataCore.Entities;
using System;
using System.Collections.Generic;

namespace DataCore
{
    public class ArticleDB: IArticleDB
    {
        public List<Article> GetArticle()
        {
            var article1 = new Article { Id = 0, Title = "Zivot savcu", Synopsis = "Bla", Content = "Savci ziji" };
            var article2 = new Article { Id = 0, Title = "Zivot savcu", Synopsis = "Bla", Content = "Savci ziji" };
            var article3 = new Article { Id = 0, Title = "Zivot savcu", Synopsis = "Bla", Content = "Savci ziji" };
            var article4 = new Article { Id = 0, Title = "Zivot savcu", Synopsis = "Bla", Content = "Savci ziji" };

            var output = new List<Article> { article1, article2, article3, article4 };

            return output;
        }
    }
}
