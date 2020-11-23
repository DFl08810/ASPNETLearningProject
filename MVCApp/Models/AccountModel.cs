using System;
using System.Collections.Generic;
using System.Text;

namespace MVCApp.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Registered { get; set; }
        public DateTime LastLogin { get; set; }
        public int NoOfComments { get; set; }
        public int NoOfArticles { get; set; }
    }
}
