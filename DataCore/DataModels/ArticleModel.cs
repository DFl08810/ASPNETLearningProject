using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.DataModels
{
    class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Content { get; set; }
    }
}
