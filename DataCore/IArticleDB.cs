using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore
{
    public interface IArticleDB
    {
        List<Article> GetArticle();
        bool PostArticle(Article article);
    }
}
