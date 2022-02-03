using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Books.Service.Tests.Integration.Base;
using Books.Service.Web.Models;
using Books.Service.Web.Responses;
using FluentAssertions;
using Xunit;

namespace Books.Service.Tests.Integration.Web.Controllers;

[Collection("BooksControllerTests")]
public class BooksControllerTests : BaseIntegrationTest
{
    private const string endpoint = "/books";
    public BooksControllerTests() : base() { }

    [Fact]
    public async Task CreateBook_InvalidBook_ShouldReturnBadRequest()
    {
        var book = new BookDto(null!, null!, -1);

        var response = await TestHttpClient.PostObjectAsync(endpoint, book);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var responseBody = await response.Content.ReadAsObjectAsync<BadRequestResponse>();

        responseBody.Should().NotBeNull();
        responseBody!.Errors.Count().Should().Be(3);

        var errors = responseBody!.Errors.ToArray();
        errors[0].Should().Be("The field Price must be between 0 and 9999.");
        errors[1].Should().Be("The Title field is required.");
        errors[2].Should().Be("The Author field is required.");
    }

    [Fact]
    public async Task UpdateBook_InvalidBook_ShouldReturnBadRequest()
    {
        var book = new BookDto(null!, null!, -1);

        var response = await TestHttpClient.PutObjectAsync($"{endpoint}/{book.Id}", book);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseBody = await response.Content.ReadAsObjectAsync<BadRequestResponse>();

        responseBody.Should().NotBeNull();
        responseBody!.Errors.Count().Should().Be(3);

        var errors = responseBody!.Errors.ToArray();
        errors[0].Should().Be("The field Price must be between 0 and 9999.");
        errors[1].Should().Be("The Title field is required.");
        errors[2].Should().Be("The Author field is required.");
    }
}