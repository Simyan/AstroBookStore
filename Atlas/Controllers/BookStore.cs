using Application.Commands;
using Atlas.DataStore.Mocks;
using Atlas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using MediatR;

namespace Atlas.Controllers
{
    //[Route("api/[controller]")]
    [Route("api")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookStoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Booklist", Name = "Books")]
        public IEnumerable<Book>? Get(int i)
        {
            var books = Books.listOfBooks;
            return i switch
            {
                0 => books,
                > 0 => books.Where(w => w.Id == i),
                _ => null
            }; 
        }

        [HttpPost("CreateBook")]
        public Task CreateBook(CreateBookCommand createBookCommand) 
        {
            return _mediator.Send(createBookCommand);
        }
    }
}
