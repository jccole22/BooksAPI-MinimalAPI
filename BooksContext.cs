using Microsoft.EntityFrameworkCore;

namespace BooksAPI
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BooksContext(DbContextOptions<BooksContext> option) : base(option)
        {

        }
    }
}
