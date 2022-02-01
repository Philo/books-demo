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

    public async Task<Book> CreateAsync(Book book)
    {
        await _books.InsertOneAsync(book);
        return book;
    }

    public async Task DeleteAsync(long id)
        => await _books.DeleteOneAsync(x => x.Id == id);

    public async Task<Book?> GetBookAsync(long id)
        => await _books.Find(x => x.Id == id).FirstOrDefaultAsync();

    public IQueryable<Book> GetBooks()
        => _books.AsQueryable();

    public async Task<Book> UpdateAsync(Book book)
        => await _books.FindOneAndReplaceAsync(x => x.Id == book.Id, book);
}