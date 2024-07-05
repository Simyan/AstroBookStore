using Atlas.DataStore.Mocks;
using Atlas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Atlas.Controllers
{
    //[Route("api/[controller]")]
    [Route("api")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
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




    }
}
