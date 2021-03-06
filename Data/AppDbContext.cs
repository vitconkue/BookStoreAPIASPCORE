using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using System.Linq;

namespace BookStore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<BookType> BookTypes {get;set;}
       

        public DbSet<Book> Books {get;set;}
        public DbSet<BookAmountChangingRecord> BookAmountChangingRecord {get;set;}

        public DbSet<Configuration> Configurations {get;set;}
        
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Bill> Bills {get;set;}
        
        public DbSet<BillDetail> BillsDetails {get;set;}

        public DbSet<Receipt> Receipts {get;set;}   
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }

       

    }
}