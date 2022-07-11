using Umbraco.Cms.Core.Security;

namespace Umbraco.OnDOTNETLiveDemo.ViewModels
{
    public class DemoMember : MemberIdentityUser
    {
        public bool HasPerUnicorn { get; set; }
    }
}
