using Microsoft.EntityFrameworkCore;
using LibraryManagement.Database.Entities;

namespace LibraryManagement.Database.DataContext;

    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }

