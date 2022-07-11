using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net.Http;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;
using Umbraco.OnDOTNETLiveDemo.Models;
using File = System.IO.File;

namespace Umbraco.TechCommunityDayDemo.Notifications
{
    public class MediaSavedNotificationHandler : INotificationHandler<MediaSavedNotification>
    {
        private readonly MediaFileManager _mediaFileManager;
        private readonly IMediaService _mediaService;
        private readonly IConfiguration _configuration;

        public MediaSavedNotificationHandler(MediaFileManager mediaFileManager, IMediaService mediaService, IConfiguration configuration)
        {
            _mediaFileManager = mediaFileManager;
            _mediaService = mediaService;
            _configuration = configuration;
        }

        public async void Handle(MediaSavedNotification notification)
        {
            var apiKey = _configuration.GetValue<string>("ComputerVision:SubscriptionKey");
            var endpoint = _configuration.GetValue<string>("ComputerVision:EndPoint");

            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(apiKey)) { Endpoint = endpoint };           

            using (client)
            {
                foreach (var media in notification.SavedEntities)
                {
                    if (media.ContentType.Alias == Image.ModelTypeAlias && media.GetValue<string>("caption").IsNullOrWhiteSpace())
                    {
                        var imageStream = _mediaFileManager.GetFile(media, out string mediaPath);                                               

                        using (imageStream)
                        {
                            var results = await client.DescribeImageInStreamAsync(imageStream);

                            if (results != null)
                            {
                                foreach (var caption in results.Captions)
                                {
                                    var mediaItem = _mediaService.GetById(media.Id);
                                    mediaItem.SetValue("caption", caption.Text);
                                    _mediaService.Save(mediaItem);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
