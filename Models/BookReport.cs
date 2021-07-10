using System;

namespace BookStore.Models
{
    public class BookReport
    {
        public Book Book {get;set;}
        public int Before {get;set;}
        
        public int ChangeAmount {get;set;}
        public int After {get;set;}

        public DateTime Date {get;set;}
    }
}