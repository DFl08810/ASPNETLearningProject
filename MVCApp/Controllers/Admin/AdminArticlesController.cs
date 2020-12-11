using IdentityLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCApp.EntityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers.Admin
{
    [Authorize(Roles = RoleDef.Admin)]
    [Route("Admin/Articles")]
    public class AdminArticlesController : Controller
    {
        private readonly IArticleModelService _articleService;

        public AdminArticlesController(IArticleModelService articleService)
        {
            this._articleService = articleService;
        }

        public IActionResult Index()
        {
            var articles = _articleService.GetAllArticles();
            return View("../Admin/Articles/Index", articles);
        }
    }
}
