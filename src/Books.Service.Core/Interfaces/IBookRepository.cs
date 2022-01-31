using System.Linq;
using System.Threading.Tasks;
using Books.Service.Core.Entites;

namespace Books.Service.Core.Interfaces;

public interface IBookRepository
{
    Task Create(Book book);
    IQueryable<Book> GetBooks();
    Task<Book> Update(Book book);
    Task<Book> GetBook(long id);
    Task Delete(long id);
}