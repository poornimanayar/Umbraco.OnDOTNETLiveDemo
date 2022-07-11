using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common;
using Umbraco.OnDOTNETLiveDemo.ViewModels;

namespace Umbraco.TechCommunityDayDemo.ViewComponents
{
    public class SampleFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new SampleFormViewModel());
        }
    }
}
