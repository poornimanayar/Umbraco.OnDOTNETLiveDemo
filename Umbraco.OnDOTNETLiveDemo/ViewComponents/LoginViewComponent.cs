using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Models;
using Umbraco.OnDOTNETLiveDemo.ViewModels;

namespace Umbraco.TechCommunityDayDemo.ViewComponents
{
    public class LoginViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new LoginModel());
        }
    }
}
