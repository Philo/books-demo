using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoMapper;
using Books.Service.Core.Entites;
using Books.Service.Core.Requests;
using Books.Service.Web.Controllers;
using Books.Service.Web.Mappings;
using Books.Service.Web.Models;
using Books.Service.Web.Responses;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Books.Service.Tests.Unit.Web.Controllers;

public class BookControllerTests
{
        private static IMapper? _mapper;

        public BookControllerTests()
        {
            if(_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(config =>
                {
                    config.AddProfile(new MappingProfile());
                });
                _mapper = mappingConfig.CreateMapper();
            }
        }

        [Theory, AutoData]
        public async Task CreateBook_Success_ShouldReturnId(BookDto book, Book createdBook)
        {
            var mediator = A.Fake<IMediator>();
            var controller = new BooksController(A.Fake<IMapper>(), mediator);

            A.CallTo(() => mediator.Send(A<CreateBookRequest>._, new CancellationToken()))
                .Returns(createdBook);

            var response = await controller.CreateBook(book);
            var result = response.Result as ObjectResult;

            result.Should().NotBeNull();
            result!.StatusCode.Equals(StatusCodes.Status201Created);

            result.Value.Should().NotBeNull();
            var createSuccess = result.Value as CreateSuccessResponse;
            createSuccess!.Id.Should().Be(createdBook.Id);

            A.CallTo(() => mediator.Send(A<CreateBookRequest>._, new CancellationToken()))
                .MustHaveHappenedOnceExactly();            
        }

        [Theory, AutoData]
        public async Task GetBook_Success_ShouldReturnBooks(IEnumerable<Book> books)
        {
            var mediator = A.Fake<IMediator>();
            var controller = new BooksController(_mapper!, mediator);

            A.CallTo(() => mediator.Send(A<GetBooksRequest>._, new CancellationToken()))
                .Returns(books);

            var response = await controller.GetBook();
            var result = response.Result as OkObjectResult;

            result.Should().NotBeNull();
            result!.StatusCode.Equals(StatusCodes.Status200OK);

            var returnedBooks = result.Value as IEnumerable<BookDto>;
            returnedBooks!.Count().Should().Be(books.Count());

            A.CallTo(() => mediator.Send(A<GetBooksRequest>._, new CancellationToken()))
                .MustHaveHappenedOnceExactly();            
        }

        [Theory, AutoData]
        public async Task UpdateBookById_Success(long id, Book book, BookDto bookDto)
        {
            var mediator = A.Fake<IMediator>();
            var controller = new BooksController(A.Fake<IMapper>(), mediator);

            A.CallTo(() => mediator.Send(A<UpdateBookRequest>._, new CancellationToken()))
                .Returns(book);

            var response = await controller.UpdateBookById(id, bookDto);
            var result = response as OkResult;

            result.Should().NotBeNull();
            result!.StatusCode.Equals(StatusCodes.Status200OK);

            A.CallTo(() => mediator.Send(A<UpdateBookRequest>._, new CancellationToken()))
                .MustHaveHappenedOnceExactly();            
        }


        [Theory, AutoData]
        public async Task UpdateBookById_NotFound(long Id, BookDto bookDto)
        {
            var mediator = A.Fake<IMediator>();
            var controller = new BooksController(A.Fake<IMapper>(), mediator);

            A.CallTo(() => mediator.Send(A<UpdateBookRequest>._, new CancellationToken()))
                .Returns((Book?) null);

            var response = await controller.UpdateBookById(Id, bookDto);
            var result = response as NotFoundResult;

            result.Should().NotBeNull();
            result!.StatusCode.Equals(StatusCodes.Status404NotFound);

            A.CallTo(() => mediator.Send(A<UpdateBookRequest>._, new CancellationToken()))
                .MustHaveHappenedOnceExactly();            
       }

        [Theory, AutoData]
        public async Task GetBookById_Success(long id, Book book)
        {
            var mediator = A.Fake<IMediator>();
            var controller = new BooksController(_mapper!, mediator);

            A.CallTo(() => mediator.Send(A<GetBookRequest>._, new CancellationToken()))
                .Returns(book);

            var response = await controller.GetBookById(id);
            var result = response.Result as OkObjectResult;

            result.Should().NotBeNull();
            result!.StatusCode.Equals(StatusCodes.Status200OK);

            result.Value.Should().NotBeNull();
            var returnedBook = result.Value as BookDto;
            returnedBook.Should().BeEquivalentTo(book);

            A.CallTo(() => mediator.Send(A<GetBookRequest>._, new CancellationToken()))
                .MustHaveHappenedOnceExactly();            
        }


        [Theory, AutoData]
        public async Task GetBookById_NotFound(long Id)
        {
            var mediator = A.Fake<IMediator>();
            var controller = new BooksController(A.Fake<IMapper>(), mediator);

            A.CallTo(() => mediator.Send(A<GetBookRequest>._, new CancellationToken()))
                .Returns((Book?) null);

            var response = await controller.GetBookById(Id);
            var result = response.Result as NotFoundResult;

            result.Should().NotBeNull();
            result!.StatusCode.Equals(StatusCodes.Status404NotFound);

            A.CallTo(() => mediator.Send(A<GetBookRequest>._, new CancellationToken()))
                .MustHaveHappenedOnceExactly();            
       }

       [Theory, AutoData]
        public async Task DeleteBookById_Success(long id, Book book)
        {
            var mediator = A.Fake<IMediator>();
            var controller = new BooksController(_mapper!, mediator);

            A.CallTo(() => mediator.Send(A<DeleteBookRequest>._, new CancellationToken()))
                .Returns(book);

            var response = await controller.DeleteBookById(id);
            var result = response as OkResult;

            result.Should().NotBeNull();
            result!.StatusCode.Equals(StatusCodes.Status200OK);

            A.CallTo(() => mediator.Send(A<DeleteBookRequest>._, new CancellationToken()))
                .MustHaveHappenedOnceExactly();            
        }


        [Theory, AutoData]
        public async Task DeleteBookById_NotFound(long Id)
        {
            var mediator = A.Fake<IMediator>();
            var controller = new BooksController(A.Fake<IMapper>(), mediator);

            A.CallTo(() => mediator.Send(A<DeleteBookRequest>._, new CancellationToken()))
                .Returns((Book?) null);

            var response = await controller.DeleteBookById(Id);
            var result = response as NotFoundResult;

            result.Should().NotBeNull();
            result!.StatusCode.Equals(StatusCodes.Status404NotFound);

            A.CallTo(() => mediator.Send(A<DeleteBookRequest>._, new CancellationToken()))
                .MustHaveHappenedOnceExactly();            
       }
}