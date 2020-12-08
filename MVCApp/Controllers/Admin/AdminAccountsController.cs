using IdentityLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCApp.EntityServices;
using MVCApp.Models;
using MVCApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers.Admin
{
    //Controller for accounts actions in admininistration page
    [Authorize(Roles = RoleDef.Admin)]
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

        [Route("Admin/Accounts")]
        public IActionResult Index()
        {
            var accounts = _accountService.GetAllAccounts();
            
            return View("../Admin/Accounts", accounts);
        }


        [HttpGet]
        [Route("Admin/Accounts/Edit/{Id?}")]
        public IActionResult Edit(int Id)
        {
            var account = _accountService.GetAccount(Id);
            return View("../Admin/Edit", account);
        }

        [HttpPost]
        [Route("Admin/Accounts/Edit/{Account?}")]
        public IActionResult Edit(AccountModel account)
        {
            return View("../Admin/Edit");
        }
    }
}
