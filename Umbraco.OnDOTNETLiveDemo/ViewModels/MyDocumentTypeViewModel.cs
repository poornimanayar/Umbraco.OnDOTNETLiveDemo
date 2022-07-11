using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.OnDOTNETLiveDemo.Models;

namespace Umbraco.OnDOTNETLiveDemo.ViewModels
{
    public class MyDocumentTypeViewModel : MyDocumentType
    {
        public MyDocumentTypeViewModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }


        public string MyProperty { get; set; }

        public List<Article>? Articles { get; set; }

    }
}
