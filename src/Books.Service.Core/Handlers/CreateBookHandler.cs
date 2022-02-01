using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;
using Books.Service.Core.Requests;
using MediatR;

namespace Books.Service.Core.Handlers;

public class CreateBookHandler : IRequestHandler<CreateBookRequest, Book>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book> Handle(CreateBookRequest request, CancellationToken cancellationToken)
        => await _bookRepository.CreateAsync(request.NewBook);
}