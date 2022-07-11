using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.OnDOTNETLiveDemo.Models;
using Umbraco.OnDOTNETLiveDemo.Services;
using Umbraco.OnDOTNETLiveDemo.ViewModels;

namespace Umbraco.TechCommunityDayDemo.Controllers
{
    public class MyDocumentTypeController : RenderController
    {
        private readonly IVariationContextAccessor _variationContextAccessor;
        private readonly ServiceContext _serviceContext;
        private readonly IMyService _myService;
        private readonly UmbracoHelper _umbracoHelper;
        public MyDocumentTypeController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor, ServiceContext serviceContext, IMyService myService, UmbracoHelper umbracoHelper) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _variationContextAccessor = variationContextAccessor;
            _serviceContext = serviceContext;
            _myService = myService;
            _umbracoHelper = umbracoHelper;
        }

        public override IActionResult Index()
        {
            //get content root (home node in our case)
            var contentRoot = _umbracoHelper.ContentAtRoot().FirstOrDefault();

            //get the first child of the ArticleList type, in my content tree ArticleList is allowed as a child to the home node
            var articleList = contentRoot!.FirstChild<ArticleList>();
            //customise the model with additional properties from other parts of Umbraco content tree or external integrations
            var model = new MyDocumentTypeViewModel(CurrentPage!, new PublishedValueFallback(_serviceContext, _variationContextAccessor))
            {
                MyProperty = _myService.ResultFromMyAwesomeIntegration(),
                Articles = _myService.GetAllArticles(articleList!.Id)
            };

            return CurrentTemplate(model);
        }
    }
}
