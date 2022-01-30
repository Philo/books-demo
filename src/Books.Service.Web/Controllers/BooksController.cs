using System.ComponentModel.DataAnnotations;
using Books.Service.Web.Models;
using Books.Service.Web.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Books.Service.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{   
    [HttpPost()]
    [Consumes(typeof(Book), "application/json")]
    [SwaggerOperation(OperationId = "create-book", Summary = "Creates a new book")]
    [SwaggerResponse(StatusCodes.Status201Created, "Created", typeof(CreateSuccessResponse), "application/json")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(BadRequestResponse), "application/json")]
    public ActionResult<Book> CreateBook([Required] Book book) 
    {
        throw new NotImplementedException();
    }      

    [HttpGet()] 
    [SwaggerOperation(OperationId = "get-book", Summary = "Returns a list of books. Sorted by title by default.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(Book), "application/json")]
    public IEnumerable<Book> GetBook(string sortby)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    [Consumes(typeof(Book), "application/json")]
    [SwaggerOperation(OperationId = "update-book-by-id", Summary = "Update an existing book")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad Request", typeof(BadRequestResponse), "application/json")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
    public ActionResult UpdateBookById([Required] long id, [Required] [FromBody] Book book)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    [SwaggerOperation(OperationId = "get-book-by-id", Summary = "Gets a book by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success", typeof(Book), "application/json")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
    public ActionResult GetBookById([Required] long id)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(OperationId = "delete-book-by-id", Summary = "Deletes a book by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Book not found")]
    public ActionResult DeleteBookById([Required] long id)
    {
        throw new NotImplementedException();
    }
}
