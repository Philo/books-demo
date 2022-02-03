
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Books.Service.Tests.Integration.Web;

public class BaseWebIntegrationTest
{
    public HttpClient TestHttpClient { get; private set;}
    public ServiceProvider ServiceProvider { get; private set; }

    public BaseWebIntegrationTest()
    {
        var application = new WebApplicationFactory<Program>();

        ServiceProvider = (ServiceProvider)application.Services;
        TestHttpClient = application.CreateClient();
    }
}