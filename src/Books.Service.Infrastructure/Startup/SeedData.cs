using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Books.Service.Infrastructure.Startup;

public class DbInitializer
{
    private readonly IBookRepository _books;
    private readonly ILogger _logger;

    public DbInitializer(IBookRepository bookRepository, ILogger<DbInitializer> logger)
    {
        _books = bookRepository;
        _logger = logger;
    }

    public async Task Seed()
    {
        _logger.LogInformation("Beginning data seed");
        foreach(var book in GetSeedData())
        {
            var existingBook = await _books.GetBookAsync(book.Id);
            if(existingBook == null)
            {
                _logger.LogInformation("Seeding item {id}", book.Id);
                await _books.CreateAsync(book);
            } 
            else 
            {
                _logger.LogInformation("Item {id} already exists, skipping", book.Id);
            }
        }
    }

    private static IEnumerable<Book> GetSeedData() 
        => new List<Book>
        {
            new Book("Winnie-the-pooh", "A. A. Milne", 19.25M){ Id = 1 },
            new Book("Pride and Prejudice", "Jane Austin", 5.49M){ Id = 2 },
            new Book("Romeo and Juliet", "William Shakespeare", 6.95M){ Id = 3 }
        };
}