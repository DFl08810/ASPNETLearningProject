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
        //alias that is used in application
        public string DisplayNickName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
