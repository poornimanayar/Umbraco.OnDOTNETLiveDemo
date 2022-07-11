
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.OnDOTNETLiveDemo.Models;

namespace Umbraco.OnDOTNETLiveDemo.Services;

public interface IMyService
{
    string GetSomeString();

    string ResultFromMyAwesomeIntegration();

    List<Article>? GetAllArticles(int parentId);


}

public class MyService : IMyService
{
    private readonly UmbracoHelper _umbracoHelper;

    private readonly ILogger<MyService> _logger;

    public MyService(UmbracoHelper umbracoHelper, ILogger<MyService> logger)
    {
        _umbracoHelper = umbracoHelper;
        _logger = logger;
    }

    public List<Article>? GetAllArticles(int parentId)
    {
        return _umbracoHelper.Content(parentId)?.Children<Article>()?.ToList();
    }

    public string GetSomeString()
    {
        _logger.LogInformation("Executing GetSomeString()");
        return "Hello Umbraco!!!";
    }

    public string ResultFromMyAwesomeIntegration()
    {
        return "Result from my awesome integration";
    }

    
}
