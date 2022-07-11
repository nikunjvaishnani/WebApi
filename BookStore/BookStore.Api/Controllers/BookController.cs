using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/Book")]
    public class BookController : Controller
    {
        BookRepository _repository = new BookRepository();

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddBook(BookModel model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            Book book = new Book()
            {
                BookId = model.BookId,
                Name = model.Name,  
                Price = model.Price,
                Description = model.Description,
                PublisherId = model.PublisherId,
                CategoryId = model.CategoryId,  
                Quantity = model.Quantity,
            };

            book = _repository.AddBook(book);
            BookModel bookModel = new BookModel(book);   
            return Ok(bookModel);
        }

        [HttpGet]
        [Route("GetBooks")]
        [ProducesResponseType(typeof(BookModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetBooks(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var books = _repository.GetBooks(pageIndex, pageSize, keyword);

            ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
            {
                Results = books.Results.Select(c => new BookModel(c)),
                TotalRecords = books.TotalRecords,
            };

            return Ok(listResponse);
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(BookModel),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateBook(BookModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Book book = new Book()
            {
                BookId = model.BookId,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                PublisherId = model.PublisherId,
                CategoryId = model.CategoryId,
                Quantity = model.Quantity,
            };

            book = _repository.UpdateBook(book);
            BookModel bookModel = new BookModel(book);
            return Ok(bookModel);
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult DeleteBook(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            bool isDeleted = _repository.DeleteBook(id); 
            return Ok(isDeleted);
        }
    }
}
