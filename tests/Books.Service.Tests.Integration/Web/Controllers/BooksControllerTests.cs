using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;
using Books.Service.Tests.Integration.Base;
using Books.Service.Web.Models;
using Books.Service.Web.Responses;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Books.Service.Tests.Integration.Web.Controllers;

public class BooksControllerTests : BaseIntegrationTest
{
    private const string endpoint = "/books";
    private readonly IBookRepository _booksRepository;
    public BooksControllerTests() : base() 
    {
        _booksRepository = ServiceProvider.GetRequiredService<IBookRepository>();
    }

    [Theory, AutoData]
    public async Task CreateBook_Success(BookDto book)
    {
        var response = await TestHttpClient.PostObjectAsync(endpoint, book);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var responseBody = await response.Content.ReadAsObjectAsync<CreateSuccessResponse>();

        responseBody.Should().NotBeNull();
        responseBody!.Id.Should().Be(1);

        await _booksRepository.DeleteAsync(1);
    } 

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

    [Theory, AutoData]
    public async Task GetBooks_Success(Book expected)
    {
        await _booksRepository.CreateAsync(expected);

        var response = await TestHttpClient.GetAsync(endpoint);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadAsObjectAsync<IEnumerable<BookDto>>();

        responseBody.Should().NotBeNull();
        responseBody!.Count().Should().Be(1);
        responseBody!.First().Should().BeEquivalentTo(expected);

        await _booksRepository.DeleteAsync(expected.Id);
    }

    [Theory, AutoData]
    public async Task UpdateBookById_Success(BookDto newBook, Book existingBook)
    {
        await _booksRepository.CreateAsync(existingBook);

        var response = await TestHttpClient.PutObjectAsync($"{endpoint}/{existingBook.Id}", newBook);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var actualBook = await _booksRepository.GetBookAsync(1);

        actualBook!.Id.Should().Be(existingBook.Id);
        actualBook.Title.Should().Be(newBook.Title);
        actualBook.Author.Should().Be(newBook.Author);
        actualBook.Price.Should().Be(newBook.Price);

        await _booksRepository.DeleteAsync(existingBook.Id);
    }

    [Fact]
    public async Task UpdateBookById_InvalidBook_ShouldReturnBadRequest()
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

    [Theory, AutoData]
    public async Task UpdateBookById_BookDoesNotExist_ShouldReturnNotFound(BookDto book)
    {
        var response = await TestHttpClient.PutObjectAsync($"{endpoint}/{book.Id}", book);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory, AutoData]
    public async Task GetBookById_Success(Book book)
    {
        book.Id = 1;

        await _booksRepository.CreateAsync(book);

        var response = await TestHttpClient.GetAsync($"{endpoint}/{book.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadAsObjectAsync<BookDto>();
        responseBody.Should().BeEquivalentTo(book);

        await _booksRepository.DeleteAsync(book.Id);
    }

    [Theory, AutoData]
    public async Task GetBookById_BookDoesNotExist_ShouldReturnNotFound(BookDto book)
    {
        var response = await TestHttpClient.GetAsync($"{endpoint}/{book.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Theory, AutoData]
    public async Task DeleteBookById_Success(Book book)
    {
        book.Id = 1;

        await _booksRepository.CreateAsync(book);

        var response = await TestHttpClient.DeleteAsync($"{endpoint}/{book.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var actual = await _booksRepository.GetBookAsync(book.Id);
        actual.Should().BeNull();
    }

    [Theory, AutoData]
    public async Task DeleteBookById_BookDoesNotExist_ShouldReturnNotFound(BookDto book)
    {
        var response = await TestHttpClient.DeleteAsync($"{endpoint}/{book.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}