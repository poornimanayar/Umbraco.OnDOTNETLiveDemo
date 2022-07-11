using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Web;

namespace Umbraco.OnDOTNETLiveDemo.Notifications
{
    public class ContentPublishedNotificationHandler : INotificationHandler<ContentPublishedNotification>
    {
        private readonly ILogger<ContentPublishedNotificationHandler> _logger;

        public ContentPublishedNotificationHandler(ILogger<ContentPublishedNotificationHandler> logger)
        {
            _logger = logger;
        }

        public void Handle(ContentPublishedNotification notification)
        {

            foreach (var entity in notification.PublishedEntities)
            {
                //send to external apis etc
                _logger.LogInformation("Message from notification handler for content published notification - {nodeName}", entity.Name);
            }

        }
    }
}
