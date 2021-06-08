using BookStore.ActionModels;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        List<BookType> GetAllType();
        List<Book> GetAllBooks(); 
        Book GetById(int id); 


        BookAmountChangingRecord SaveNewBookAmountChangingRecord(int BookId, int AmountChanging, bool IsImport);
        Book AddBook(AddBookModel newBook);  

        EntityEntry DeleteById(int id);

        Book UpdateBook(UpdateBookActionModel changed);
    }
}