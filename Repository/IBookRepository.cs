using BookStore.Models; 
using System.Collections.Generic;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks(); 
        Book GetById(int id);  
    }
}