using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common;

namespace Umbraco.TechCommunityDayDemo.ViewComponents
{
    public class SampleViewComponent : ViewComponent
    {
        private readonly UmbracoHelper _umbracoHelper;

        public SampleViewComponent(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        public IViewComponentResult Invoke()
        {
            //get the root node(s) and choose the first one (as we have only one root node)
            var homeNode = _umbracoHelper.ContentAtRoot().FirstOrDefault();
            return View(homeNode.Id);
        }
    }
}
