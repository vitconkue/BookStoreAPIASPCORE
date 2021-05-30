using BookStore.Data;
using BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookStore.ActionModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<BookType> GetAllType()
        {
            return _context.BookTypes.ToList();
        }

        public Book AddBook(AddBookModel newBook)
        {
            Book toAdd = new Book
            {
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
            return _context.Books.OrderBy(book => book.Id).Include(book => book.Type).ToList();
        }

        public List<Book> SearchBooks(string searchString)
        {
            return _context.Books.Where(book => book.Title.Contains(searchString)).ToList();
        }

        public Book GetById(int id)
        {
            Book result = _context.Books.Include(book => book.Type).FirstOrDefault(book => book.Id == id);
            return result;
        }

        public EntityEntry DeleteById(int id)
        {
            Book found = _context.Books.Include(book => book.Type).FirstOrDefault(book => book.Id == id);
            if (found == null)
            {
                return null;
            }
            var result = _context.Books.Remove(found);
            _context.SaveChanges();
            return result;
        }

        public Book UpdateBook(UpdateBookActionModel changed)
        {
            Book found = _context.Books.FirstOrDefault(book => book.Id == changed.Id);
            if (found == null)
            {
                return null;
            }

            BookType type = _context.BookTypes.FirstOrDefault(type => type.Id == changed.TypeID);
            if (type == null)
            {
                return null;
            }

            found.Title = changed.Title;
            found.Author = changed.Author;
            found.CurrentAmount = changed.Amount;
            found.Type = type;

            _context.SaveChanges();

            return found;
        }
    }
}