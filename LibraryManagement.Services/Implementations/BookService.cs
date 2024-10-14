using AutoMapper;
using LibraryManagement.Services.Dtos;
using LibraryManagement.Database.Entities;
using LibraryManagement.Database.Interfaces;
using LibraryManagement.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, IMapper mapper, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<ResponseBookDto> GetAllBooks()
        {
            try
            {
                IEnumerable<Book> books = _bookRepository.GetAllBooks();
                return _mapper.Map<IEnumerable<ResponseBookDto>>(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting all books: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<ResponseBookDto> GetBooksByPages(RequestPaginationDto pageDetails)
        {

            try
            {
                int totalBooks = _bookRepository.GetTotalBooksCount();
                int totalPages = (int)Math.Ceiling((double)totalBooks / pageDetails.PageSize);

                if (pageDetails.PageNumber <= 0 || pageDetails.PageNumber > totalPages)
                {
                    _logger.LogWarning($"Invalid page number requested: {pageDetails.PageNumber}. Total pages: {totalPages}");
                    return Enumerable.Empty<ResponseBookDto>();
                }

                PagingMetadata pagingMetadata = _mapper.Map<PagingMetadata>(pageDetails);
                IEnumerable<Book> books = _bookRepository.GetBooksByPages(pagingMetadata);
                return _mapper.Map<IEnumerable<ResponseBookDto>>(books);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Invalid operation while fetching books: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error occurred while getting requested page books: {ex.Message}");
                throw;
            }
        }


        public IEnumerable<ResponseBookDto> GetAllBooksBySort(String sortBy)
        {
            try
            {
                IEnumerable<Book> books = _bookRepository.GetAllBooks();
                if(sortBy.ToLower().Equals("author"))
                {
                    books=books.OrderBy(book=>book.Author);
                    return _mapper.Map<IEnumerable<ResponseBookDto>>(books);
                }
                else if(sortBy.ToLower().Equals("title"))
                {
                    books=books.OrderBy(book=>book.Title);
                    return _mapper.Map<IEnumerable<ResponseBookDto>>(books);
                }
                else
                {
                    throw new InvalidOperationException("Enter correct criteria"); 
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting sorted books: {ex.Message}");
                throw;

            }

        }

        public ResponseBookDto GetBookById(int id)
        {
            try
            {
                Book book = _bookRepository.GetBookById(id);
                return _mapper.Map<ResponseBookDto>(book);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Book not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting book with ID {id}: {ex.Message}");
                throw;
            }
        }

        public int AddBook(RequestBookDto bookDto)
        {
            try
            {
                Book book = _mapper.Map<Book>(bookDto);
                return _bookRepository.AddBook(book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding a new book: {ex.Message}");
                throw;
            }
        }

        public void UpdateBook(int id , RequestBookDto bookDto)
        {
            try
            {
                Book book = _bookRepository.GetBookById(id);
                _mapper.Map(bookDto, book);
                _bookRepository.UpdateBook(book);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Book not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating book with ID {id}: {ex.Message}");
                throw;
            }


        }

        public void DeleteBook(int id)
        {
            try
            {
                _bookRepository.DeleteBook(id);
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException("Book not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting book with ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
