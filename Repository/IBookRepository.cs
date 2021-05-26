using BookStore.ActionModels;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks(); 
        Book GetById(int id); 

        Book AddBook(AddBookModel newBook);  

        EntityEntry DeleteById(int id);

        Book UpdateBook(UpdateBookActionModel changed);
    }
}