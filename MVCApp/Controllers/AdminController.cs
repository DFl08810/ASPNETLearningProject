using IdentityLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCApp.EntityServices;
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
        #endregion
        #region ctor
        public AdminController(IArticleService articleService)
        {
            this._articleService = articleService;
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

    }
}
