using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;
using Books.Service.Core.Requests;
using MediatR;

namespace Books.Service.Core.Handlers;

public class GetBookHandler : IRequestHandler<GetBookRequest, Book?>
{
    private readonly IBookRepository _bookRepository;

    public GetBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book?> Handle(GetBookRequest request, CancellationToken cancellationToken)
        => await _bookRepository.GetBookAsync(request.Id);
}