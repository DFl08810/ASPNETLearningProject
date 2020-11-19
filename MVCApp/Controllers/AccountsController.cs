using IdentityLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ICredentialsService _credentialService;

        public AccountsController(ICredentialsService credentialsService)
        {
            this._credentialService = credentialsService;
        }

        [BindProperty]
        public AccountInputModel Input { get; set; }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountInputModel inputModel)
        {
            var loginResult = _credentialService.ProcessLogin(Input);
            if (!loginResult.Result)
            {
                inputModel = new AccountInputModel { ErrorState = true };
                return View(inputModel);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
