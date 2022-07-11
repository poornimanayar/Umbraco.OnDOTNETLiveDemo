using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.OnDOTNETLiveDemo.Models;
using Umbraco.OnDOTNETLiveDemo.ViewModels;

namespace Umbraco.OnDOTNETLiveDemo.Controllers
{
    public class FactSurfaceController : SurfaceController
    {
        private readonly IContentService _contentService;

        private readonly UmbracoHelper _umbracoHelper;
        public FactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, IContentService contentService, UmbracoHelper umbracoHelper) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _contentService = contentService;
            _umbracoHelper = umbracoHelper;
        }


        public IActionResult CreateFact(FactViewModel model)
        {
            //validations omitted, but make sure you use validations in production code

            //get the content root
            var contentRoot = _umbracoHelper.ContentAtRoot().FirstOrDefault();

            //get the fact listing node, using tree traversal
            var factListing = contentRoot.FirstChild<FactList>();

            //create a document
            var fact = _contentService.Create(model.FactName, factListing.Id, Fact.ModelTypeAlias);

            //set custom properties
            fact.SetValue("content", model.FactContent);

            fact.SetValue("contentPublisher", "code");


            //save and publish the document
            _contentService.SaveAndPublish(fact);

            TempData["success"] = true;

            return CurrentUmbracoPage();
        }
    }
}
