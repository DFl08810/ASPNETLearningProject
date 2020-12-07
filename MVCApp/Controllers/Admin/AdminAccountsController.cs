using IdentityLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers.Admin
{
    [Authorize(Roles = RoleDef.Admin)]
    [Route("Admin/Accounts")]
    public class AdminAccountsController : Controller
    {
        public AdminAccountsController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
