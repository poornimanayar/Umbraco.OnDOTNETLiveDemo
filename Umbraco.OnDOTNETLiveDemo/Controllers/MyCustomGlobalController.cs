using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Umbraco.OnDOTNETLiveDemo.Controllers
{
    public class MyCustomGlobalController : RenderController
    {
        private readonly ILogger<MyCustomGlobalController> _logger;
        public MyCustomGlobalController(ILogger<MyCustomGlobalController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _logger = logger;
        }

        public override IActionResult Index()
        {
            _logger.LogInformation("Executing MyCustomGlobalController/Index");
            return CurrentTemplate(CurrentPage);
        }
    }
}
