using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLib.Models
{
    public class RoleDef
    {
        public const string Admin = "Admin";
        public const string User = "User";
        
        //Function that lists all roles defined in this  class
        public static IEnumerable<string> EnumRoles()
        {
            var enumeratedRoles = new List<string>();
            enumeratedRoles.Add("Admin");
            enumeratedRoles.Add("User");

            return enumeratedRoles;
        }
    }
}
