using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Web;

namespace Umbraco.OnDOTNETLiveDemo.Notifications
{
    public class ContentPublishingNotificationHandler : INotificationHandler<ContentPublishingNotification>
    {
        private readonly ILogger<ContentPublishingNotificationHandler> _logger;

        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public ContentPublishingNotificationHandler(ILogger<ContentPublishingNotificationHandler> logger, IUmbracoContextFactory umbracoContextFactory)
        {
            _logger = logger;
            _umbracoContextFactory = umbracoContextFactory;
        }

        public void Handle(ContentPublishingNotification notification)
        {
            foreach (var entity in notification.PublishedEntities)
            {
                _logger.LogInformation("Message from notification handler for content publishing - {nodeName}", entity.Name);
             }
        }

            
    }
}
