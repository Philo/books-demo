using System.Linq;
using System.Threading.Tasks;
using Books.Service.Core.Entites;

namespace Books.Service.Core.Interfaces;

public interface IBookRepository
{
    Task<Book> CreateAsync(Book book);
    IQueryable<Book> GetBooks();
    Task<Book> UpdateAsync(Book book);
    Task<Book> GetBookAsync(long id);
    Task DeleteAsync(long id);
}