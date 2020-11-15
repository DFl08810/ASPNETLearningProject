using DataCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCore.Prefabs
{
    public class ArticlePrefab
    {
        private readonly Article _article;

        public ArticlePrefab(Article article)
        {
            this._article = article;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Synopsis { get; private set; }
        public string Content { get; private set; }

        public ArticlePrefab ConvertObject()
        {
            Id = _article.Id;
            Title = _article.Title;
            Synopsis = _article.Synopsis;
            Content = _article.Content;

            return this;
        }
    }
}

