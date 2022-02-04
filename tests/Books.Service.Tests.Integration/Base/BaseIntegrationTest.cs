using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Xunit;

namespace Books.Service.Tests.Integration.Base;

[Collection("BooksIntegrationTests")]
public class BaseIntegrationTest : IDisposable
{
    public HttpClient TestHttpClient { get; private set;}
    public ServiceProvider ServiceProvider { get; private set; }

    public BaseIntegrationTest()
    {
        var application = new TestWebApplicationFactory();

        ServiceProvider = (ServiceProvider)application.Services;
        TestHttpClient = application.CreateClient();
    }

    public void Dispose()
    {
        TestHttpClient.Dispose();
        ServiceProvider.Dispose();

        GC.SuppressFinalize(this);
    }
}