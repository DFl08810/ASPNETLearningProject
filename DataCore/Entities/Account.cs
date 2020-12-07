using System;
using System.Collections.Generic;
using System.Text;

namespace DataCore.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public bool IsPending { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Registered { get; set; }
        public DateTime LastLogin { get; set; }
        public int NoOfComments { get; set; }
        public int NoOfArticles { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
