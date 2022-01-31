
using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;
using Books.Service.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Books.Service.Infrastructure.Repositories;

public class MongoBookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _books;

    public MongoBookRepository(IOptions<DatabaseSettings> settings)
    {
        var mongoClient = new MongoClient(
            settings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            settings.Value.DatabaseName);

        _books = mongoDatabase.GetCollection<Book>(settings.Value.BooksCollectionName);
    }

    public Task<Book> Create(Book book)
    {
        throw new System.NotImplementedException();
    }

    public Task Delete(long Id)
    {
        throw new System.NotImplementedException();
    }

    public Task<Book> GetBook(long Id)
    {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Book>> GetBooks()
    {
        throw new System.NotImplementedException();
    }

    public Task<Book> Update(long Id, Book book)
    {
        throw new System.NotImplementedException();
    }
}