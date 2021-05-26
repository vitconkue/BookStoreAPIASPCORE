using BookStore.Data;
using BookStore.Models; 
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookStore.ActionModels;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private  readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public Book AddBook(AddBookModel newBook)
        {
            Book toAdd = new Book {
                Title = newBook.Title,
                Author = newBook.Author,
                CurrentAmount = newBook.Amount
            };
            BookType type = _context.BookTypes.FirstOrDefault(type => type.Id == newBook.TypeID);
          
            toAdd.Type = type; 
            _context.Add(toAdd);
            _context.SaveChanges();  
            return toAdd;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.Include(book => book.Type).ToList();
        }

        public Book GetById(int id)
        {
            Book result = _context.Books.Include(book => book.Type).FirstOrDefault(book => book.Id == id);
            return result; 
        }
    }
}