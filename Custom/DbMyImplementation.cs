// using LibraryManagement.Database.Interfaces;
// using LibraryManagement.Database.Entities;
// using LibraryManagement.Database.DataContext;


// namespace LibraryManagementSystem.Database.Implementations
// {
//     public class BookRepository : IBookRepository
//     {
//         private readonly BookDbContext _bookDbContext;

//         public BookRepository(BookDbContext bookDbContext)
//         {
//             _bookDbContext = bookDbContext;
//         }

//         public IEnumerable<Book> GetAllBooks()
//         {
//             return _bookDbContext.Books.ToList();
//         }

//         public Book GetBookById(int id)
//         {
//             return _bookDbContext.Books.Find(id);
//         }

//         public void AddBook(Book book)
//         {
//             _bookDbContext.Books.Add(book);
//             _bookDbContext.SaveChanges();
//         }

//         public void UpdateBook(Book book)
//         {
//             _bookDbContext.Books.Update(book);
//             _bookDbContext.SaveChanges();
//         }

//         public void DeleteBook(int id)
//         {
//             Book book = _bookDbContext.Books.Find(id);
//             _bookDbContext.Books.Remove(book);
//             _bookDbContext.SaveChanges();
//         }
//     }
// }