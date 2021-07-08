using System;
using BookStore.Models;

namespace BookStore.DTO
{
    public class BillDetailDTO
    {

        public int BillDetailId {get;set;}

        public BookDTO Book {get;set;}

        public int Price {get;set;}
        public int Amount {get;set;}

        public BillDetailDTO(BillDetail source)
        {
            BillDetailId = source.BillDetailId; 
            Price = source.Price;
            Amount = source.Amount;
            Book = new BookDTO(source.Book);
        }

        
    }
}