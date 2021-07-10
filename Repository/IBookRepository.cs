using BookStore.ActionModels;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        List<BookType> GetAllType();
        List<Book> GetAllBooks(string typeId, string searchString);

        List<BookAmountChangingRecord> GetAllBookAmountChangingRecords();
        Book GetById(int id); 


        BookAmountChangingRecord SaveNewBookAmountChangingRecord(int BookId, int AmountChanging, bool IsImport);
        Book AddBook(AddBookModel newBook);  

        EntityEntry DeleteById(int id);

        Book UpdateBook(UpdateBookActionModel changed);

        // for report part

        List<int> GetBooksIdWithChangedAmountInMonth(int month, int year);
        List<BookAmountChangingRecord> GetBookAmountChangingRecordsByMonthAndBookId(int bookId, int month,int year); 
    }
}