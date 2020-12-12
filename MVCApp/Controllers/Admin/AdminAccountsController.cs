using IdentityLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCApp.EntityServices;
using MVCApp.Models;
using MVCApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Controllers.Admin
{
    //Controller for accounts actions in admininistration page
    [Authorize(Roles = RoleDef.Admin)]

    public class AdminAccountsController : Controller
    {
        private readonly IAccountModelService _accountService;
        private readonly ICredentialsService _credentialsService;
        public AdminAccountsController(IAccountModelService accountService,
                                ICredentialsService credentialsService)
        {

            this._accountService = accountService;
            this._credentialsService = credentialsService;
        }

        [Route("Admin/Accounts")]
        public IActionResult Index()
        {
            var accounts = _accountService.GetAllAccounts();

            return View("../Admin/Accounts/Index", accounts);
        }


        [HttpGet]
        [Route("Admin/Accounts/Edit/{Id?}")]
        public IActionResult Edit(int Id)
        {
            var account = _accountService.GetAccount(Id);
            return View("../Admin/Accounts/Edit", account);
        }

        [HttpPost]
        [Route("Admin/Accounts/Update/{Account?}")]
        public IActionResult Update(AccountModel Account)
        {
            var accountModel = _accountService.GetAccount(Account.Id);

            if (string.IsNullOrEmpty(Account.Role) || string.IsNullOrEmpty(Account.Email))
            {
                return View("../Admin/Accounts/Edit", accountModel);
            }


            //Chekcs if email or role is different than current database entry
            if(accountModel.Email != Account.Email || accountModel.Role != Account.Role)
            {
                accountModel.Email = Account.Email;
                accountModel.Role = Account.Role;


                var user = _credentialsService.GetUser(accountModel.Id);
                user.Email = Account.Email;


                var identityResult = _credentialsService.UpdateUser(user, Account.Role, this.User);
                if(identityResult)
                {
                    var resultAccount = _accountService.UpdateAccount(accountModel);
                    return View("../Admin/Accounts/Edit", resultAccount);
                }
                
            }

            return View("../Admin/Accounts/Edit", accountModel);
        }

        [HttpGet]
        [Route("Admin/Accounts/SearchUser")]
        public IActionResult SearchUser(string searchQuery)
        {
            var resultAccounts = _accountService.GetMatchingAccounts(searchQuery);

            return PartialView("../Admin/Accounts/_AccountsPartial", resultAccounts);
        }


        [HttpGet]
        [Route("Admin/Accounts/SortBy")]
        public IActionResult SortBy(string sortMode)
        {
            //sort and  return
            //if sortMode is null, no mode is tripped and unsorted list is returned
            var result = _accountService.SortAllAccounts(sortMode);
            return PartialView("../Admin/Accounts/_AccountsPartial", result);
        }

        [HttpGet]
        [Route("Admin/Accounts/Delete")]
        public IActionResult Delete(int Id)
        {
            //remove user from identity database
            var eventRes = _credentialsService.DeleteUser(Id, this.User);
            //delete user from main database
            if (eventRes)
            {
                _accountService.DeleteAccount(Id);
                return StatusCode(200, "User has been deleted");

            }
            return StatusCode(403, "Cannot delete this user");
        }

        [Route("Admin/Accounts/Synchronize")]
        public IActionResult Synchronize()
        {
            //retrieves all users in identity database

            var usersFromIdentity = _credentialsService.RetrieveUsers().Result;

            //performs synchronization of all users registered in app
            var resultAccounts = _accountService.Synchronize(usersFromIdentity);
            return View("../Admin/Accounts/Index", resultAccounts);
        }

        [HttpGet]
        [Route("Admin/Accounts/Accept/{Id?}")]
        public IActionResult Accept(int Id)
        {
            var account = _accountService.GetAccount(Id);
            account.IsPending = false;
            _accountService.UpdateAccount(account);
            return ViewComponent("AccountValidation", account);
        }

        [HttpGet]
        [Route("Admin/Accounts/Disable/{Id?}")]
        public IActionResult Disable(int Id)
        {
            var result = _credentialsService.SetUserStatus(Id, false);
            if (result)
            {
                var account = _accountService.GetAccount(Id);
                account.Status = false;
                _accountService.UpdateAccount(account);
                return ViewComponent("AccountValidation", account);
            }
            else
            {
                return StatusCode(403, "Cannot change status on this user.");
            }
        }

        [HttpGet]
        [Route("Admin/Accounts/Enable/{Id?}")]
        public IActionResult Enable(int Id)
        {

            var result = _credentialsService.SetUserStatus(Id, true);
            if (result)
            {
                var account = _accountService.GetAccount(Id);
                account.Status = true;
                _accountService.UpdateAccount(account);
                return ViewComponent("AccountValidation", account);
            }
            else
            {
                return StatusCode(403, "Cannot change status on this user.");
            }
        }
    }
}
