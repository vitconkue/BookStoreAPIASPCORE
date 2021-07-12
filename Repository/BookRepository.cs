using BookStore.Data;
using BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookStore.ActionModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BookStore.Helper;

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
        
        public BookAmountChangingRecord SaveNewBookAmountChangingRecord(int BookId, int AmountChanging, bool IsImport)
        {
            var foundBook = _context.Books.FirstOrDefault(book => book.Id == BookId);
            
            if(foundBook == null)
                return null;
            int currentAmountBeforeChange = foundBook.CurrentAmount;
            var record = new BookAmountChangingRecord
            {
                Book = foundBook,
                AmountBeforeChanged = currentAmountBeforeChange,
                AmountChanged = AmountChanging,
                IsImport = IsImport
            };

            _context.Add(record);
            _context.SaveChanges();
            return record;
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

        public List<Book> GetAllBooks(string typeId, string searchString)
        {
            List<Book> result = new List<Book>();
            // apply type filter
            if(typeId != null)
            {
                bool parseResult = int.TryParse(typeId, out int parseTypeId);
                if(!parseResult)
                    return null;
                 result = 
                    _context.Books.Include(book => book.Type)
                                  .Where(book => book.Type.Id == parseTypeId)
                                  .OrderBy(book => book.Id)
                                  .ToList();
            }
            else{
                result = _context.Books.Include(book => book.Type)
                                    .OrderBy(book => book.Id)
                                  .ToList();
            }

            // apply search filter
            if(searchString != null && searchString != string.Empty)
            {
                result = result
                .Where(book => book.Title.ToLower().Contains(searchString.ToLower()) || 
                HelperFunctions.RemovedUTFAndToLower(book.Title).Contains(HelperFunctions.RemovedUTFAndToLower(searchString))).ToList(); 

                // sort result
                result = result
                .OrderByDescending(searchResult
                     => HelperFunctions.rateSearchResult(searchString,searchResult.Title))
                .ToList();
            }

            return result; 

             
        }

        public List<BookAmountChangingRecord> GetAllBookAmountChangingRecords()
        {
            return _context
            .BookAmountChangingRecord
            .Include(record => record.Book)
            .OrderByDescending(record => record.DateChanged).ToList();
        }

        public Book GetById(int id)
        {
            Book result = _context.Books.Include(book => book.Type).FirstOrDefault(book => book.Id == id);
            return result;
        }

        public EntityEntry DeleteById(int id)
        {
            // delete in bookamount changing record

            
            Book found = _context.Books.Include(book => book.Type).
                                        Include(book => book.InBillDetails)
                                        .Include(book => book.InBookAmountChangingRecord)
                                    
                        .FirstOrDefault(book => book.Id == id);
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
            int currentAmountBeforeChange = found.CurrentAmount; 

            // change in book amount
            if(changed.Amount != currentAmountBeforeChange)
            {
                bool isImport  = changed.Amount > currentAmountBeforeChange;
                int amountChanging  = 0 ; 
                if(changed.Amount > currentAmountBeforeChange)
                {
                    amountChanging = changed.Amount - currentAmountBeforeChange;
                }
                else{
                    amountChanging = currentAmountBeforeChange - changed.Amount ;

                }
                var recordAdd = SaveNewBookAmountChangingRecord(found.Id,amountChanging, isImport); 

            }
            
            BookType newType = _context.BookTypes.FirstOrDefault(type => type.Id == changed.TypeID);
            if (newType == null)
            {
                return null;
            }
            found.Title = changed.Title;
            found.Author = changed.Author;
            found.Type = newType;
            found.CurrentAmount = changed.Amount;

            
            _context.SaveChanges();

            return found;
        }

        public List<BookAmountChangingRecord> GetBookAmountChangingRecordsByMonthAndBookId(int bookId, int month, int year)
        {
           List<BookAmountChangingRecord> records = new List<BookAmountChangingRecord>(); 
           records = _context
            .BookAmountChangingRecord
            .Include(record => record.Book)
            .Where(record => record.Book.Id == bookId 
            && record.DateChanged.Month == month && record.DateChanged.Year == year)
            .OrderByDescending(record => record.DateChanged)
            .ToList(); 

            
           return records;

        }

        public List<int> GetBooksIdWithChangedAmountInMonth(int month, int year)
        {
            var result = new List<int>(); 
             List<int> ToAdd  = _context.BookAmountChangingRecord
                    .Include(record => record.Book)
                    .Select(record => record.Book.Id)
                    .ToList(); 
            if(ToAdd != null && ToAdd.Count > 0 )
            {
                result.AddRange(ToAdd);
            }
            ToAdd = _context
                .BillsDetails
                .Include(detail => detail.Book)
                .Include(detail => detail.Bill)
                .Where(detail => detail.Bill.DateTime.Month == month && detail.Bill.DateTime.Year == year)
                .Select(detail => detail.Book.Id).ToList();
            

            if(ToAdd != null && ToAdd.Count > 0 )
            {
                result.AddRange(ToAdd);
            }

            result = result.Distinct().ToList();
            return result;
        }
    }
}