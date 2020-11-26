using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> Synchronize(List<Account> accounts);
        IEnumerable<Account> SaveRange(List<Account> accounts);
    }
}
