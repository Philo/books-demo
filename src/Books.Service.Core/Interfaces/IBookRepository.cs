

using System.Collections.Generic;
using System.Threading.Tasks;
using Books.Service.Core.Entites;

namespace Books.Service.Core.Interfaces;

public interface IBookRepository
{
    Task<Book> Create(Book book);
    Task<IEnumerable<Book>> GetBooks();
    Task<Book> Update(long Id, Book book);
    Task<Book> GetBook(long Id);
    Task Delete(long Id);
}