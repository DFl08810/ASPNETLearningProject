using IdentityLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCApp.EntityServices;
using MVCApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers.Admin
{
    //Controller for accounts actions in admininistration page
    [Authorize(Roles = RoleDef.Admin)]
    [Route("Admin/Accounts")]
    public class AdminAccountsController : Controller
    {
        private readonly IAccountModelService _accountService;
        private readonly ICredentialsService _credentialsService;
        public AdminAccountsController(IAccountModelService accountService,
                                ICredentialsService credentialsService)
        {

            this._accountService = accountService;
            this._credentialsService = credentialsService;
        }

        public IActionResult Index()
        {
            var accounts = _accountService.GetAllAccounts();
            
            return View("../Admin/Accounts", accounts);
        }
    }
}
