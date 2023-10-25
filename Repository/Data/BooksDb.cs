using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository.Data
{
    public class BooksDb : DbContext
    {
        public BooksDb(DbContextOptions<BooksDb> options)
            : base(options)
        {

        }

        public DbSet<Book> Books => Set<Book>();
    }
}
