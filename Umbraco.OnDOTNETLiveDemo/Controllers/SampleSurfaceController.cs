using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.OnDOTNETLiveDemo.ViewModels;

namespace Umbraco.OnDOTNETLiveDemo.Controllers
{
    public class SampleSurfaceController : SurfaceController
    {
        private readonly ILogger<SampleSurfaceController> _logger;
        public SampleSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, ILogger<SampleSurfaceController> logger) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSampleForm(SampleFormViewModel model)
        {
            //do a simple logging here, but can call a service to do database saves or even integrate with external systems 
            _logger.LogInformation("Sample form submitted - {firstname} {lastname} {email} {truefalse}", model.FirstName, model.LastName, model.Email, model.SomeTrueFalseValue);

            TempData["Success"] = true;

            return CurrentUmbracoPage();
        }
    }
}
