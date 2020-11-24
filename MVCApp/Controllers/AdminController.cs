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

namespace MVCApp.Controllers
{
    [Authorize(Roles = RoleDef.Admin)]
    public class AdminController : Controller
    {
        #region fields
        private IArticleModelService _articleService;
        private readonly IAccountModelService _accountService;
        private readonly ICredentialsService _credentialsService;

        #endregion
        #region ctor
        public AdminController(IArticleModelService articleService,
                                IAccountModelService accountService,
                                ICredentialsService credentialsService)
        {
            this._articleService = articleService;
            this._accountService = accountService;
            this._credentialsService = credentialsService;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Articles()
        {
            var articles = _articleService.FetchArticles();
            return View(articles);
        }

        public IActionResult Accounts()
        {
            var accounts = _accountService.GetAllAccounts();
            return View(accounts);
        }


        public IActionResult Synchronize()
        {
            //retrieves all users in identity database
            var usersFromIdentity = _credentialsService.RetrieveUsers().Result;

            //performs synchronization of all users registered in app
            var resultAccounts = _accountService.Synchronize(usersFromIdentity);
            return View("Accounts", resultAccounts);
        }

        [HttpGet]
        public IActionResult SearchUser(string searchQuery)
        {
            var resultAccounts = _accountService.GetMatchingAccounts(searchQuery);

            return PartialView("_AccountsPartial", resultAccounts);
        }
    }
}
