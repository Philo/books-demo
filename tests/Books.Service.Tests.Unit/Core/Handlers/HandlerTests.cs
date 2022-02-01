using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Books.Service.Core.Entites;
using Books.Service.Core.Enums;
using Books.Service.Core.Handlers;
using Books.Service.Core.Interfaces;
using Books.Service.Core.Requests;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Books.Service.Tests.Unit.Core.Handlers;

public class HandlerTests
{
    [Theory, AutoData]
    public async Task CreateBookHandler_ShouldCallCreateBook(Book book)
    {
        var bookRepository = A.Fake<IBookRepository>();
        var handler = new CreateBookHandler(bookRepository);

        var createdBook = await handler.Handle(new CreateBookRequest(book), new CancellationToken());

        A.CallTo(() => bookRepository.CreateAsync(book))
            .MustHaveHappenedOnceExactly();
    }

    [Theory, AutoData]
    public async Task DeleteBookHandler_BookExists_ShouldCallDeleteBook(Book book)
    {
        var bookRepository = A.Fake<IBookRepository>();
        var handler = new DeleteBookHandler(bookRepository);

        A.CallTo(() => bookRepository.GetBookAsync(book.Id))
            .Returns(book);

        var result = await handler.Handle(new DeleteBookRequest(book.Id), new CancellationToken());

        result.Should().NotBeNull();

        A.CallTo(() => bookRepository.DeleteAsync(book.Id))
            .MustHaveHappenedOnceExactly();
    }

    [Theory, AutoData]
    public async Task DeleteBookHandler_BookDoesNotExist_ShouldReturnNull(long bookId)
    {
        var bookRepository = A.Fake<IBookRepository>();
        var handler = new DeleteBookHandler(bookRepository);

        A.CallTo(() => bookRepository.GetBookAsync(bookId))
            .Returns((Book?) null);

        var result = await handler.Handle(new DeleteBookRequest(bookId), new CancellationToken());

        result.Should().BeNull();
    }

    [Theory, AutoData]
    public async Task GetBookHandler_ShouldCallGetBook(long bookId)
    {
        var bookRepository = A.Fake<IBookRepository>();
        var handler = new GetBookHandler(bookRepository);

        await handler.Handle(new GetBookRequest(bookId), new CancellationToken());

        A.CallTo(() => bookRepository.GetBookAsync(bookId))
            .MustHaveHappenedOnceExactly();
    }

    [Theory, AutoData]
    public async Task GetBooksHandler_ShouldBeOrderedByTitle(Book bookA, Book bookB, Book bookC)
    {
        bookA.Title = "A";
        bookB.Title = "B";
        bookC.Title = "C";

        var books = new List<Book>{ bookB, bookC, bookA };

        var bookRepository = A.Fake<IBookRepository>();
        var handler = new GetBooksHandler(bookRepository);

        A.CallTo(() => bookRepository.GetBooks())
            .Returns(books.AsQueryable());

        var result = await handler.Handle(new GetBooksRequest(SortBy.Title), new CancellationToken());
        var bookArray = result.ToArray();

        bookArray[0].Should().BeEquivalentTo(bookA);
        bookArray[1].Should().BeEquivalentTo(bookB);
        bookArray[2].Should().BeEquivalentTo(bookC);

        A.CallTo(() => bookRepository.GetBooks())
            .MustHaveHappenedOnceExactly();
    }

    [Theory, AutoData]
    public async Task GetBooksHandler_ShouldBeOrderedByAuthor(Book bookA, Book bookB, Book bookC)
    {
        bookA.Author = "A";
        bookB.Author = "B";
        bookC.Author = "C";

        var books = new List<Book>{ bookB, bookC, bookA };

        var bookRepository = A.Fake<IBookRepository>();
        var handler = new GetBooksHandler(bookRepository);

        A.CallTo(() => bookRepository.GetBooks())
            .Returns(books.AsQueryable());

        var result = await handler.Handle(new GetBooksRequest(SortBy.Author), new CancellationToken());
        var bookArray = result.ToArray();

        bookArray[0].Should().BeEquivalentTo(bookA);
        bookArray[1].Should().BeEquivalentTo(bookB);
        bookArray[2].Should().BeEquivalentTo(bookC);

        A.CallTo(() => bookRepository.GetBooks())
            .MustHaveHappenedOnceExactly();
    }

    [Theory, AutoData]
    public async Task GetBooksHandler_ShouldBeOrderedByPrice(Book bookA, Book bookB, Book bookC)
    {
        bookA.Price = 1M;
        bookB.Price = 2.2M;
        bookC.Price = 3M;

        var books = new List<Book>{ bookB, bookC, bookA };

        var bookRepository = A.Fake<IBookRepository>();
        var handler = new GetBooksHandler(bookRepository);

        A.CallTo(() => bookRepository.GetBooks())
            .Returns(books.AsQueryable());

        var result = await handler.Handle(new GetBooksRequest(SortBy.Price), new CancellationToken());
        var bookArray = result.ToArray();

        bookArray[0].Should().BeEquivalentTo(bookA);
        bookArray[1].Should().BeEquivalentTo(bookB);
        bookArray[2].Should().BeEquivalentTo(bookC);

        A.CallTo(() => bookRepository.GetBooks())
            .MustHaveHappenedOnceExactly();
    }

    [Theory, AutoData]
    public async Task UpdateBookHandler_BookExists_ShouldCallUpdateBook(Book book)
    {
        var bookRepository = A.Fake<IBookRepository>();
        var handler = new UpdateBookHandler(bookRepository);

        A.CallTo(() => bookRepository.GetBookAsync(book.Id))
            .Returns(book);

        var result = await handler.Handle(new UpdateBookRequest(book), new CancellationToken());

        result.Should().NotBeNull();

        A.CallTo(() => bookRepository.UpdateAsync(book))
            .MustHaveHappenedOnceExactly();
    }

    [Theory, AutoData]
    public async Task UpdateBookHandler_BookDoesNotExist_ShouldReturnNull(Book book)
    {
        var bookRepository = A.Fake<IBookRepository>();
        var handler = new UpdateBookHandler(bookRepository);

        A.CallTo(() => bookRepository.GetBookAsync(book.Id))
            .Returns((Book?) null);

        var result = await handler.Handle(new UpdateBookRequest(book), new CancellationToken());

        result.Should().BeNull();
    }
}