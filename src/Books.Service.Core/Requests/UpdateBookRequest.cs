
using Books.Service.Core.Entites;
using MediatR;

namespace Books.Service.Core.Requests;

public class UpdateBookRequest : IRequest<Book?>
{
    public Book NewBook { get; init; }

    public UpdateBookRequest(Book newBook)
    {
        NewBook = newBook;
    }
}