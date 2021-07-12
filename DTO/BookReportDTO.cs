using System;
using BookStore.Models;

namespace BookStore.DTO
{
    public class BookReportDTO 
    {
        public BookDTO Book {get;set;}
        public int Before {get;set;}
        
        public int ChangeAmount {get;set;}
        public int After {get;set;}

        public DateTime Date {get;set;}

        public BookReportDTO(BookReport source)
        {
            Book = new BookDTO(source.Book);
            ChangeAmount = source.ChangeAmount; 
            After = source.After;
            Date = source.Date;
            Before = source.Before;
        }
}
}