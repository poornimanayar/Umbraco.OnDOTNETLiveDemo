using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Umbraco.OnDOTNETLiveDemo.ViewModels;

namespace Umbraco.TechCommunityDayDemo.ViewComponents
{
    public class RegisterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new RegisterViewModel());
        }
    }
}
