using CommandCore.Prefabs;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> SaveRange(List<Article> articles);
        Article SaveNew(Article article);
        bool DeleteArticle(int Id);
    }
}
