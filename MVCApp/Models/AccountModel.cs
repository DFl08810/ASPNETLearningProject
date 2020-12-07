using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MVCApp.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public bool IsPending { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Username")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public DateTime Registered { get; set; }
        [Display(Name = "Last Login")]
        public DateTime LastLogin { get; set; }
        [Display(Name = "Comments Count")]
        public int NoOfComments { get; set; }
        [Display(Name = "ArticlesCount")]
        public int NoOfArticles { get; set; }
    }
}
