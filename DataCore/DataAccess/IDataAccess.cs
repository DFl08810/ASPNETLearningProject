using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCore.DataAccess
{
    public interface IDataAccess<T>
        where T : class
    {
        T SelectById(int id);
        IEnumerable<T> SelectAll();
        bool Save(T obj);
        bool SaveRange(IEnumerable<T> obj);
        IEnumerable<T> MatchByString(string matchString);
        int Commit();
        bool UpdateRange(IEnumerable<T> obj);
    }
}
