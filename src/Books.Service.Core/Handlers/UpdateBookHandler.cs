using Books.Service.Core.Entites;
using Books.Service.Core.Interfaces;
using Books.Service.Core.Requests;
using MediatR;

namespace Books.Service.Core.Handlers;

public class UpdateBookHandler : IRequestHandler<UpdateBookRequest, Book?>
{
    private readonly IBookRepository _bookRepository;

    public UpdateBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Book?> Handle(UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookAsync(request.NewBook.Id);
        if(book != null)
        {
            await _bookRepository.UpdateAsync(request.NewBook);
        }

        return book;
    }
}