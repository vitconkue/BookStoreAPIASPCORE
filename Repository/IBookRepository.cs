using BookStore.ActionModels;
using BookStore.Models; 
using System.Collections.Generic;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks(); 
        Book GetById(int id); 

        Book AddBookRepository(AddBookModel newBook);  
    }
}