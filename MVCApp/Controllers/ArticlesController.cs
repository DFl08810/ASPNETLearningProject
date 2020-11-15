using Microsoft.AspNetCore.Mvc;
using MVCApp.EntityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class ArticlesController : Controller
    {
        private IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            this._articleService = articleService;
        }

        public IActionResult List()
        {
            var articles = _articleService.FetchArticles();
            return View(articles);
        }
    }
}
