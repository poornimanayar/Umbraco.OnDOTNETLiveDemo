using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.OnDOTNETLiveDemo.Models;

namespace Umbraco.OnDOTNETLiveDemo.Controllers
{
    //routed as /umbraco/backoffice/api/SampleBackOfficeApi/{action}
       public class SampleBackofficeApiController : UmbracoAuthorizedApiController
    {
        private readonly UmbracoHelper _umbracoHelper;

        public SampleBackofficeApiController(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        public List<string> GetFacts()
        {
            //get content root
            var root = _umbracoHelper.ContentAtRoot().FirstOrDefault();

            //get the first child of type FactList
            var factsListing = root.FirstChild<FactList>();

            //get all children of type fact
            var facts = factsListing.Children<Fact>();

            var listToReturn = new List<string>();

            foreach(var fact in facts)
            {
                listToReturn.Add(fact.Content);
            }

            return listToReturn;
        }
    }
}
