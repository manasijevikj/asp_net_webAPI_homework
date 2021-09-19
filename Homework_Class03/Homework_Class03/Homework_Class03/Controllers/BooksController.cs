using Homework_Class03.Controllers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Class03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }


        [HttpGet("{index}")]
        public ActionResult<Book> Get(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request");
                }
                if (index > StaticDb.Books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Resource with index {index} does not exist!");
                }
                return Ok(StaticDb.Books[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("filter")]
        public ActionResult<List<Book>> FilterBooks(string author, string title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request you must enter at least one parameter");
                }

                if(string.IsNullOrEmpty(author))
                {
                    List<Book> books = StaticDb.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                    return StatusCode(StatusCodes.Status200OK, books);
                }

                if (string.IsNullOrEmpty(title))
                {
                    List<Book> books = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())).ToList();
                    return StatusCode(StatusCodes.Status200OK, books);
                }

                List<Book> filterdBooks = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower()) && x.Title.ToLower().Contains(title.ToLower())).ToList();
                return StatusCode(StatusCodes.Status200OK, filterdBooks);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("postbook")]
        public IActionResult postBook([FromBody] Book book)
        {
            try
            {
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "Book is created");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost("listOfTitles")]
        public ActionResult<List<string>> postBooks([FromBody] List<Book> books)
        {
            try
            {
                List<string> titles = books.Select(x => x.Title).ToList();
                return StatusCode(StatusCodes.Status200OK, titles);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
