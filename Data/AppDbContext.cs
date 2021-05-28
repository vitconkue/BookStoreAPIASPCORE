using Microsoft.EntityFrameworkCore;
using BookStore.Models; 

namespace BookStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<BookType> BookTypes {get;set;}

        public DbSet<Book> Books {get;set;}

        public DbSet<Configuration> Configurations {get;set;}

       
    }
}