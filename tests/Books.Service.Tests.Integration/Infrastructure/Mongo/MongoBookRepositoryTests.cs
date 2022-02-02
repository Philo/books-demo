using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Books.Service.Tests.Integration.Infrastructure.Mongo;

public class MongoBookRepositoryTests : BaseIntegrationTest
{
    private readonly IBookRepository _booksRepository;

    public MongoBookRepositoryTests() : base()
    {
        _booksRepository = ServiceProvider!.GetRequiredService<IBookRepository>();
    }

    [Theory, AutoData]
    public async Task CreateBook(Book expected)
    {
        var actual = await _booksRepository.CreateAsync(expected);

        actual.Id.Should().Be(1);
        actual.Title.Should().BeEquivalentTo(expected.Title);
        actual.Author.Should().BeEquivalentTo(expected.Author);
        actual.Price.Should().Be(expected.Price);

        await _booksRepository.DeleteAsync(expected.Id);
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
}