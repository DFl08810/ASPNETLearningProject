using CommandCore.Prefabs;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Factories
{
    public interface IArticleFactory
    {
        Article GetArticle(int Id);
        IEnumerable<Article> GetAll();
    }
}
