using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLib.Models
{
    public class User : IdentityUser
    {
        public string DisplayNickName { get; set; }
    }
}
