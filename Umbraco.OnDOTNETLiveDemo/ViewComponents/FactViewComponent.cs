using Microsoft.AspNetCore.Mvc;
using Umbraco.OnDOTNETLiveDemo.ViewModels;

namespace Umbraco.OnDOTNETLiveDemo.ViewComponents
{
    public class FactViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new FactViewModel());
        }
    }
}
