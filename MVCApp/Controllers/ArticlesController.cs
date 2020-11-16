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
        #region fields
        private IArticleService _articleService;
        #endregion
        #region ctor
        public ArticlesController(IArticleService articleService)
        {
            this._articleService = articleService;
        }
        #endregion

        //This list view is intended for admins 
        public IActionResult List()
        {
            //Gets articles from database through internal logic
            var articles = _articleService.FetchArticles();
            return View(articles);
        }
    }
}
