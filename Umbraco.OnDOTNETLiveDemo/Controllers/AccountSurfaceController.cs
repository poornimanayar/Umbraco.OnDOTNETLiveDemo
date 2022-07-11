using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Models;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Extensions;
using Umbraco.OnDOTNETLiveDemo.ViewModels;

namespace Umbraco.TechCommunityDayDemo.Controllers
{
    public class AccountSurfaceController : SurfaceController
    {
        private readonly IMemberService _memberService;
        private readonly IMemberManager _memberManager;
        private readonly IMemberSignInManager _memberSignInManager;
        private readonly IPublishedContentQuery _publishedContentQuery;
        private readonly ILogger<AccountSurfaceController> _logger;

        public AccountSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, IMemberService memberService, IMemberManager memberManager, IMemberSignInManager memberSignInManager, IPublishedContentQuery publishedContentQuery, ILogger<AccountSurfaceController> logger, UserManager<MemberIdentityUser> userManager) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _memberService = memberService;
            _memberManager = memberManager;
            _memberSignInManager = memberSignInManager;
            _publishedContentQuery = publishedContentQuery;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            //check whether the user is already registered
            if (await _memberManager.FindByEmailAsync(model.Email) != null)
            {
                TempData["Success"] = false;
                TempData["errorMessage"] = "User already registered!";
                return CurrentUmbracoPage();
            }

            //create a new identity member instance without an identity
            var identityMember = MemberIdentityUser.CreateNew(model.Email, model.Email, "member", true, model.FirstName + " " + model.LastName);

            //create an identity member
            var identityResult = await _memberManager.CreateAsync(identityMember, model.Password);

            if (identityResult.Succeeded)
            {
                TempData["Success"] = true;

                if (model.HasAPetUnicorn)
                {
                    //add to role
                    await _memberManager.AddToRolesAsync(identityMember, new string[] { "unicorn user group" });

                    //save the additional details to the member profile using the MemberService
                    var member = _memberService.GetByKey(identityMember.Key);
                    member.SetValue("hasAUnicorn", model.HasAPetUnicorn);
                    _memberService.Save(member);
                }
            }
            else
            {
                TempData["Success"] = false;

                var errors = identityResult.Errors;

                var errorString = new StringBuilder();

                //added for learning purposes. This could be logged
                foreach (var error in errors)
                {
                    errorString.Append($"{error.Code}-{error.Description}");
                }

                _logger.LogError(errorString.ToString());
            }

            return CurrentUmbracoPage();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please provide username and password");
                return CurrentUmbracoPage();
            }

            //validate credentials without logging in
            var validCredentials = await _memberManager.ValidateCredentialsAsync(model.Username, model.Password);

            if (validCredentials)
            {
                //sign in
                var loginResult = await _memberSignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, true);


                if (loginResult.Succeeded)
                {                   
                    return Redirect("/my-password-protected-page/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to log in.");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Wrong credentials");
            }

            return CurrentUmbracoPage();
        }

        public async Task<IActionResult> Logout()
        {
            await _memberSignInManager.SignOutAsync();
            return Redirect(_publishedContentQuery.ContentAtRoot().FirstOrDefault().Url());
        }

    }
}
