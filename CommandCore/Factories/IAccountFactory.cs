using DataCore.Entities;
using IdentityLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Factories
{
    public interface IAccountFactory
    {
        List<Account> ConstructAccounts(List<User> users);

        List<Account> ConstructFromDb();
    }
}
