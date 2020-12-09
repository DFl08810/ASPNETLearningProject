using IdentityLib.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICredentialsService _credentialService;

        public AccountController(ICredentialsService credentialsService)
        {
            this._credentialService = credentialsService;
        }

        [BindProperty]
        public LoginModel Input { get; set; }

        //Returns login form
        public IActionResult Login()
        {
            return View();
        }

        //Manages login attempts
        [HttpPost]
        public IActionResult Login(LoginModel inputModel)
        {
            //calls service that performs login loginc in identity framework
            var loginResult = _credentialService.ProcessLogin(Input);
            //checks if login is succcess
            if (!loginResult.Result)
            {
                //this is used in case of fail mode
                inputModel = new LoginModel { ErrorState = true };
                //created account input model with error status true is passed to login form
                return View(inputModel);
            }
            //in success mode raturn to home view
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegistrationModel registration)
        {
            //Create user with disabled account
            //Requests are accepted by users with admin roles
            var result = _credentialService.MakeRegisterRequest(registration);

            var loginResult = _credentialService.ProcessLogin(new LoginModel { UserName = registration.UserName, Password = registration.Password, RememberMe = false });
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Settings()
        {
            return View();
        }

    }
}
