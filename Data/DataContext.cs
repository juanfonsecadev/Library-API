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

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loan { get; set; }

    }
}