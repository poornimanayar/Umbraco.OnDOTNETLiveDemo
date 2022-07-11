using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.OnDOTNETLiveDemo.Models;

namespace Umbraco.OnDOTNETLiveDemo.Controllers
{
    //routed as /umbraco/api/SampleApi/{action}
    //can use attribute routing
    //[Route("sample/[action]")]
    public class SampleApiController : UmbracoApiController
    {
        private readonly UmbracoHelper _umbracoHelper;

        public SampleApiController(UmbracoHelper umbracoHelper)
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
