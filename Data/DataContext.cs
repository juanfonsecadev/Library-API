using Microsoft.EntityFrameworkCore;
using Library.API.Models;

namespace Library.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        
        }

       public DbSet<Book> Books { get; set; }
public DbSet<Author> Authors { get; set; }
public DbSet<User> Users { get; set; }
public DbSet<Loan> Loans { get; set; }

    }
}