using Books.Service.Core.Entites;
using MediatR;

namespace Books.Service.Core.Requests;

public class DeleteBookRequest : IRequest<Book?>
{
    public long Id { get; init; }

    public DeleteBookRequest(long id)
    {
        Id = id;
    }
}

