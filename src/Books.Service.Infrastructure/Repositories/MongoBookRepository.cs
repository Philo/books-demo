using System.Linq;
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

    public async Task Create(Book book)
        => await _books.InsertOneAsync(book);

    public async Task Delete(long id)
        => await _books.DeleteOneAsync(x => x.Id == id);

    public async Task<Book> GetBook(long id)
        => await _books.Find(x => x.Id == id).FirstOrDefaultAsync();

    public IQueryable<Book> GetBooks()
        => _books.AsQueryable();

    public async Task<Book> Update(Book book)
        => await _books.FindOneAndReplaceAsync(x => x.Id == book.Id, book);
}