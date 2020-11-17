using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.DataAccess
{
    public interface IArticleDataAccess
    {
        Article SelectById(int id);
        List<Article> SelectAll();
        bool Save(Article article);
        int Commit();
    }
}
