using LibraryManagement.Database.Entities;
using LibraryManagement.Services.Dtos;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ResponseBookDto>> GetBooksByPage([FromQuery] RequestPaginationDto paginationDto)
        {
            var books = _bookService.GetBooksByPages(paginationDto);
            return Ok(books);
        }

        [HttpGet("sort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ResponseBookDto>> GetAllBooksBySort([FromQuery] string sortBy)
        {
            var books = _bookService.GetAllBooksBySort(sortBy);
            return Ok(books);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ResponseBookDto> GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ResponseBookDto> AddBook(RequestBookDto bookDto)
        {
            var newBookId = _bookService.AddBook(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = newBookId }, newBookId);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateBook(int id, RequestBookDto bookDto)
        {
            _bookService.UpdateBook(id, bookDto);
            return StatusCode(204, $"Book inserted successfully.");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return StatusCode(204, $"Book with ID {id} deleted successfully.");
        }
    }
}



// using LibraryManagement.Database.Entities;
// using LibraryManagement.Services.Dtos;
// using LibraryManagement.Services.Interfaces;
// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;

// namespace LibraryManagementSystem.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]/[action]")]
//     public class BooksController : ControllerBase
//     {
//         private readonly IBookService _bookService;
//         private readonly ILogger<BooksController> _logger;

//         public BooksController(IBookService bookService, ILogger<BooksController> logger)
//         {
//             _bookService = bookService;
//             _logger = logger;
//         }

//         [HttpGet]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         public ActionResult<IEnumerable<ResponseBookDto>> GetBooksByPage([FromQuery] RequestPaginationDto paginationDto)
//         {
//             try
//             {
//                 IEnumerable<ResponseBookDto> books = _bookService.GetBooksByPages(paginationDto);
//                 return Ok(books);
//             }
//             catch (InvalidOperationException ex)
//             {
//                 _logger.LogError(ex, "Invalid pagination parameters provided");
//                 return BadRequest(ex.Message);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Unexpected error occurred while retrieving books");
//                 return StatusCode(500, "An unexpected error occurred while retrieving books,");
//             }
//         }
        


//         [HttpGet("sort")]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         public ActionResult<IEnumerable<ResponseBookDto>> GetAllBooksBySort([FromQuery] string sortBy)
//         {
//             try
//             {
//                 IEnumerable<ResponseBookDto> books = _bookService.GetAllBooksBySort(sortBy);
//                 return Ok(books);
//             }
//             catch (ArgumentException ex)
//             {
//                 return BadRequest(ex.Message);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error occurred while retrieving sorted books");
//                 return StatusCode(500, "An unexpected error occurred while retrieving sorted books.");
//             }
//         }

//         [HttpGet("{id}")]
//         [ProducesResponseType(StatusCodes.Status200OK)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         public ActionResult<ResponseBookDto> GetBookById(int id)
//         {
//             try
//             {
//                 ResponseBookDto book = _bookService.GetBookById(id);
//                 return Ok(book);
//             }
//             catch (KeyNotFoundException)
//             {
//                 return NotFound($"Book with ID {id} not found.");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error occurred while retrieving book with ID {BookId}", id);
//                 return StatusCode(500, $"An unexpected error occurred while retrieving book with ID {id}.");
//             }
//         }

//         [HttpPost]
//         [ProducesResponseType(StatusCodes.Status201Created)]
//         [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         public ActionResult<ResponseBookDto> AddBook(RequestBookDto bookDto)
//         {
//             try
//             {
//                 int newBookId = _bookService.AddBook(bookDto);
//                 return CreatedAtAction(nameof(GetBookById), new { id = newBookId }, newBookId);
//             }
//             catch (ArgumentException ex)
//             {
//                 return BadRequest(ex.Message);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error occurred while adding a new book");
//                 return StatusCode(500, "An unexpected error occurred while adding the book.");
//             }
//         }

//         [HttpPut("{id}")]
//         [ProducesResponseType(StatusCodes.Status204NoContent)]
//         [ProducesResponseType(StatusCodes.Status400BadRequest)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         public IActionResult UpdateBook(int id, RequestBookDto bookDto)
//         {
//             try
//             {
//                 _bookService.UpdateBook(id, bookDto);
//                 return StatusCode(204, $"Book inserted successfully.");
//             }
//             catch (KeyNotFoundException)
//             {
//                 return NotFound($"Book with ID {id} not found.");
//             }
//             catch (ArgumentException ex)
//             {
//                 return BadRequest(ex.Message);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error occurred while updating book with ID {BookId}", id);
//                 return StatusCode(500, $"An unexpected error occurred while updating book with ID {id}.");
//             }
//         }

//         [HttpDelete("{id}")]
//         [ProducesResponseType(StatusCodes.Status204NoContent)]
//         [ProducesResponseType(StatusCodes.Status404NotFound)]
//         [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         public IActionResult DeleteBook(int id)
//         {
//             try
//             {
//                 _bookService.DeleteBook(id);
//                 return StatusCode(204, $"Book with ID {id} deleted successfully.");
//             }
//             catch (KeyNotFoundException)
//             {
//                 return NotFound($"Book with ID {id} not found.");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error occurred while deleting book with ID {BookId}", id);
//                 return StatusCode(500, $"An unexpected error occurred while deleting book with ID {id}.");
//             }
//         }

//         // [HttpGet]
//         // [ProducesResponseType(StatusCodes.Status200OK)]
//         // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//         // public ActionResult<IEnumerable<ResponseBookDto>> GetAllBooks()
//         // {
//         //     try
//         //     {
//         //         IEnumerable<ResponseBookDto> books = _bookService.GetAllBooks();
//         //         return Ok(books);
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         _logger.LogError(ex, "Error occurred while retrieving all books");
//         //         return StatusCode(500, "An unexpected error occurred while retrieving books.");
//         //     }
//         // }
        
//     }
// }