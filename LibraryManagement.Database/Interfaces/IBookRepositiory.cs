using LibraryManagement.Database.Entities;

namespace LibraryManagement.Database.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> GetBooksByPages(PagingMetadata pageDetails);
        public int GetTotalBooksCount();
        Book GetBookById(int id);
        int AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        
    }
}
