using System.Collections.Generic;
using System.Threading.Tasks;
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
            new Book{ Id = 1, Title = "Winnie-the-pooh", Author = "A. A. Milne", Price = 19.25M },
            new Book{ Id = 2, Title = "Pride and Prejudice", Author = "Jane Austin", Price = 5.49M },
            new Book{ Id = 3, Title = "Romeo and Juliet", Author = "William Shakespeare", Price = 6.95M }
        };
}