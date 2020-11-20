using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLib.Models;

namespace IdentityLib
{
    //This library is used for defining identity properties
    //This approach separates models and identity related methods from main application
    public class IdentityDataContext : IdentityDbContext<User>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //dev database, for production this database needs to be secured!!
            optionsBuilder.UseSqlite("Data Source = identity.db");
        }
    }
}
