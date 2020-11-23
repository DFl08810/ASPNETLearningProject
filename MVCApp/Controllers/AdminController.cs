using IdentityLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCApp.EntityServices;
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
        private IArticleService _articleService;
        private readonly ICredentialsService _credentialsService;
        #endregion
        #region ctor
        public AdminController(IArticleService articleService,
                                ICredentialsService credentialsService)
        {
            this._articleService = articleService;
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
            _credentialsService.RetrieveUsers();
            return View();
        }

    }
}
