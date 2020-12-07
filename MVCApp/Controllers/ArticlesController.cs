using Microsoft.AspNetCore.Mvc;
using MVCApp.EntityServices;
using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class ArticlesController : Controller
    {
        #region fields
        private IArticleModelService _articleService;
        #endregion
        #region ctor
        public ArticlesController(IArticleModelService articleService)
        {
            this._articleService = articleService;
        }
        #endregion

        //This list view is intended for admins 
        public IActionResult List()
        {
            //Gets articles from database through internal logic
            var articles = _articleService.GetAllArticles();
            return View(articles);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ArticleModel article)
        {
            var actionStatus = _articleService.PostArticle(article);
            return View();
        }
    }
}
