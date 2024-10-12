using LibraryManagement.Database.Interfaces;
using LibraryManagement.Database.Entities;
using LibraryManagement.Database.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace LibraryManagementSystem.Database.Implementations 
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _bookDbContext;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(BookDbContext bookDbContext, ILogger<BookRepository> logger)
        {
            _bookDbContext = bookDbContext;
            _logger = logger;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                return _bookDbContext.Books.ToList();
            }
            catch (SqlException sqlEx)
            {
                HandleSqlException(sqlEx, "Getting all books");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error while getting all books: {ex.Message}");
                throw;
            }
        }

        public Book GetBookById(int id)
        {
            try
            {
                var book = _bookDbContext.Books.Find(id);
                if (book == null)
                {
                    throw new KeyNotFoundException($"Book with ID {id} was not found");
                }
                return book;
            }
            catch (SqlException sqlEx)
            {
                HandleSqlException(sqlEx, $"Getting book {id}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting book {id}: {ex.Message}");
                throw;
            }
        }

        public void AddBook(Book book)
        {
            try
            {
                _bookDbContext.Books.Add(book);
                _bookDbContext.SaveChanges();
            }
            catch (SqlException sqlEx)
            {
                HandleSqlException(sqlEx, "Adding new book");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding new book: {ex.Message}");
                throw;
            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                _bookDbContext.Books.Update(book);
                _bookDbContext.SaveChanges();
            }
            catch (SqlException sqlEx)
            {
                HandleSqlException(sqlEx, $"Updating book {book.Id}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating book {book.Id}: {ex.Message}");
                throw;
            }
        }

        public void DeleteBook(int id)
        {
            try
            {
                var book = _bookDbContext.Books.Find(id);
                if (book == null)
                {
                    throw new KeyNotFoundException($"Book with ID {id} was not found");
                }
                _bookDbContext.Books.Remove(book);
                _bookDbContext.SaveChanges();
            }
            catch (SqlException sqlEx)
            {
                HandleSqlException(sqlEx, $"Deleting book {id}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting book {id}: {ex.Message}");
                throw;
            }
        }

        private void HandleSqlException(SqlException sqlEx, string operation)
        {
            _logger.LogError($"SQL Error during {operation}: {sqlEx.Message} (Error Code: {sqlEx.Number})");

            switch (sqlEx.Number)
            {
                case -2:
                    throw new TimeoutException($"Database operation timed out during {operation}", sqlEx);
                case 53:
                case 233:
                    throw new InvalidOperationException($"Database connection lost during {operation}", sqlEx);
                case 4060:
                    throw new InvalidOperationException($"Database is offline or not accessible during {operation}", sqlEx);
                case 1205:
                    throw new InvalidOperationException($"Database deadlock occurred during {operation}", sqlEx);
                case 701:
                    throw new OutOfMemoryException($"Database server out of memory during {operation}", sqlEx);
                default:
                    throw new InvalidOperationException($"Unexpected database error during {operation}", sqlEx);
            }
        }
    }
}