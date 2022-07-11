using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Umbraco.OnDOTNETLiveDemo.Controllers
{
    //implement route hijacking by creating a controller which inherits from RenderController
    //Controller name should be the name of the Document type
    public class ArticleController : RenderController
    {
        private readonly ILogger<ArticleController> _logger;
        public ArticleController(ILogger<ArticleController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _logger = logger;
        }

        //Will return the default template for the document type
        public override IActionResult Index()
        {
            _logger.LogInformation("Custom article controller");
            return CurrentTemplate(CurrentPage);
        }
    }
}
