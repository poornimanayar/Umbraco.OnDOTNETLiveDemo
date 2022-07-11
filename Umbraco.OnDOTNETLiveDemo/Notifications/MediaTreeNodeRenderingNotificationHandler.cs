using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using Umbraco.OnDOTNETLiveDemo.Models;

namespace Umbraco.TechCommunityDayDemo.Notifications
{
    public class MediaTreeNodeRenderingNotificationHandler : INotificationHandler<TreeNodesRenderingNotification>
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public MediaTreeNodeRenderingNotificationHandler(IUmbracoContextFactory umbracoContextFactory)
        {
            _umbracoContextFactory = umbracoContextFactory;
        }

        public void Handle(TreeNodesRenderingNotification notification)
        {
            if (notification.TreeAlias == Constants.Trees.Media)
            {
                var umbracoContextReference = _umbracoContextFactory.EnsureUmbracoContext();

                using (umbracoContextReference)
                {
                    foreach (var node in notification.Nodes)
                    {
                        if (int.TryParse(node.Id.ToString(), out var nodeId) && nodeId > 0)
                        {
                            var mediaItem = umbracoContextReference.UmbracoContext.Media.GetById(nodeId);
                            if (mediaItem != null)
                            {
                                if (mediaItem.ContentType.Alias == Image.ModelTypeAlias && (!mediaItem.HasValue("caption")
                                    || mediaItem.Value<string>("caption").IsNullOrWhiteSpace()))
                                {
                                    node.CssClasses.Add("caption-missing");
                                }
                            }

                        }
                    }
                }
            }            
        }
    }
}