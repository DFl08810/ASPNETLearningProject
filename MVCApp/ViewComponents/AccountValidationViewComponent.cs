using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using MVCApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.ViewComponents
{
    //Component for validating accounts
    public class AccountValidationViewComponent : ViewComponent
    {
        private readonly IAccountModelService _accountModelService;
        private readonly ICredentialsService _credentialsService;

        public AccountValidationViewComponent(IAccountModelService accountModelService)
        {
            this._accountModelService = accountModelService;
        }

        public async Task<IViewComponentResult> InvokeAsync(AccountModel accountModel)
        {
            return View(accountModel);
        }

    }
}
