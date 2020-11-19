using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class AccountInputModel
    {

            [Required]
            [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Use letters only please")]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }

            [HiddenInput(DisplayValue = false)]
            public bool ErrorState { get; set; }

    }
}
