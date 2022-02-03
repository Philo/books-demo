using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;
using Books.Service.Tests.Integration.Base;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Books.Service.Tests.Integration.Infrastructure.Mongo;

[Collection("MongoBookRepositoryTests")]
public class MongoBookRepositoryTests : BaseIntegrationTest, IAsyncDisposable
{
    private readonly IBookRepository _booksRepository;

    public MongoBookRepositoryTests() : base()
    {
        _booksRepository = ServiceProvider!.GetRequiredService<IBookRepository>();
        CleanDatabase().GetAwaiter().GetResult();
    }

    [Theory, AutoData]
    public async Task Create(Book expected)
    {
        await _booksRepository.CreateAsync(expected);
        
        var actual = await _booksRepository.GetBookAsync(expected.Id);
        
        actual.Should().NotBeNull();
        actual!.Id.Should().Be(1);
        actual.Should().BeEquivalentTo(expected);

        await _booksRepository.DeleteAsync(expected.Id);
    }

    [Theory, AutoData]
    public async Task Create_BookAlreadyExists_ShouldIncrementId(Book expected)
    {
        await _booksRepository.CreateAsync(expected);

        var actual = await _booksRepository.CreateAsync(expected);

        actual.Id.Should().Be(2);
        actual.Should().BeEquivalentTo(expected);

        await _booksRepository.DeleteAsync(1);
        await _booksRepository.DeleteAsync(expected.Id);
    }

    [Theory, AutoData]
    public async Task Delete(Book book)
    {
        await _booksRepository.CreateAsync(book);

        await _booksRepository.DeleteAsync(book.Id);

        var result = await _booksRepository.GetBookAsync(book.Id);

        result.Should().BeNull();
    }

    [Theory, AutoData]
    public async Task GetBook(Book expected)
    {
        await _booksRepository.CreateAsync(expected);

        var actual = await _booksRepository.GetBookAsync(expected.Id);

        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);

        await _booksRepository.DeleteAsync(expected.Id);
    }

    [Theory, AutoData]
    public async Task GetBook_BookDoesNotExist_ShouldReturnNull(long id)
    {
        var result = await _booksRepository.GetBookAsync(id);

        result.Should().BeNull();
    }

    [Theory, AutoData]
    public async Task GetBooks(Book bookA, Book bookB, Book bookC)
    {
        await _booksRepository.CreateAsync(bookA);
        await _booksRepository.CreateAsync(bookB);
        await _booksRepository.CreateAsync(bookC);

        var actual = _booksRepository.GetBooks().ToArray();

        actual.Length.Should().Be(3);
        actual[0].Should().BeEquivalentTo(bookA);
        actual[1].Should().BeEquivalentTo(bookB);
        actual[2].Should().BeEquivalentTo(bookC);

        await _booksRepository.DeleteAsync(bookA.Id);
        await _booksRepository.DeleteAsync(bookB.Id);
        await _booksRepository.DeleteAsync(bookC.Id);
    }

    [Fact]
    public void GetBooks_EmptyDatabase_ShouldReturnEmptyEnumerable()
    {
        var books = _booksRepository.GetBooks();

        books.Count().Should().Be(0);
    }

    [Theory, AutoData]
    public async Task Update(Book originalBook, Book expected)
    {
        expected.Id = 1;

        await _booksRepository.CreateAsync(originalBook);

        await _booksRepository.UpdateAsync(expected);

        var actual = await _booksRepository.GetBookAsync(expected.Id);

        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);

        await _booksRepository.DeleteAsync(actual!.Id);
    }

    [Theory, AutoData]
    public async Task Update_BookDoesNotExist_ShouldReturnNull(Book book)
    {
        var result = await _booksRepository.UpdateAsync(book);

        result.Should().BeNull();
    }

    public async ValueTask DisposeAsync()
    {
        await CleanDatabase();

        base.Dispose();
        GC.SuppressFinalize(this);
    }

    private async Task CleanDatabase()
    {
        var books = _booksRepository.GetBooks();
        if(books.Any())
        {
            foreach(var book in books)
            {
                await _booksRepository.DeleteAsync(book.Id);
            }
        }
    }
}