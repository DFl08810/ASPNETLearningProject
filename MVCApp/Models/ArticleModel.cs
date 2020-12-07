using CommandCore.Prefabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NoOfComments { get; set; }
    }
}
