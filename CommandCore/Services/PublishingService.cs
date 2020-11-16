using CommandCore.Prefabs;
using DataCore;
using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Services
{
    //Service for publishing articles to database
    public class PublishingService : IPublishingService
    {
        private readonly IArticleDB _articleDB;

        public PublishingService(IArticleDB articleDB)
        {
            this._articleDB = articleDB;
        }

        //Method PublishData recieves articlePrefab, validates it and post it to database in converted form
        public bool PublishData(ArticlePrefab articlePrefab)
        {
            var article = ValidateAndConvert(articlePrefab);
            var result = _articleDB.PostArticle(article);
            return true;
        }

        private Article ValidateAndConvert(ArticlePrefab articlePrefab)
        {
            //TODO: Validation logic on articlePrefab
            return new Article
            {
                Id = articlePrefab.Id,
                Title = articlePrefab.Title,
                Synopsis = articlePrefab.Synopsis,
                Content = articlePrefab.Content
            };
        }
    }
}
