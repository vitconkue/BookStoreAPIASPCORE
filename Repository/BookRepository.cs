using BookStore.Data;
using BookStore.Models; 
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private  readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetById(int id)
        {
            Book result = _context.Books.FirstOrDefault(book => book.Id == id);
            return result;
        }
    }
}