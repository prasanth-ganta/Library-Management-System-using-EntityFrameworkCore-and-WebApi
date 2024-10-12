using LibraryManagement.Database.Entities;
using LibraryManagement.Services.Dtos;

namespace LibraryManagement.Services.Interfaces
{
    public interface IBookService
    {
        IEnumerable<ResponseBookDto> GetAllBooks();
        IEnumerable<ResponseBookDto> GetBooksByPages(RequestPaginationDto pageDetails);
        ResponseBookDto GetBookById(int id);
        int AddBook(RequestBookDto bookDto);
        void UpdateBook(int ID,RequestBookDto bookDto);
        void DeleteBook(int id);
        IEnumerable<ResponseBookDto> GetAllBooksBySort(String sortBy);
    }
}
