using BookStoreBE.Models;
using BookStoreBE.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Transactions;

namespace BookStoreBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _bookRepository.GetBooks();
                return new OkObjectResult(products);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while fetching books: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _bookRepository.GetBookById(id);
                if (product != null)
                    return new OkObjectResult(product);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while fetching the book with ID {id}: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Books book)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _bookRepository.InsertBook(book);
                    scope.Complete();
                    return CreatedAtAction(nameof(Get), new { id = book.ID }, book);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while inserting the book: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Books book)
        {
            try
            {
                if (book != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        _bookRepository.UpdateBook(book);
                        scope.Complete();
                        return new OkResult();
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while updating the book: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _bookRepository.DeleteBook(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"An error occurred while deleting the book with ID {id}: {ex.Message}");

                // Return a server error response
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
