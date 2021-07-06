using System;
using BookStore.Models;

namespace BookStore.DTO
{
    public class BillDetailDTO
    {
        public BillDTO Bill {get;set;}

        public int BookID {get;set;}

        public int Price {get;set;}
        public int Amount {get;set;}

        public BillDetailDTO(BillDetail source)
        {
            Bill = new BillDTO(source.Bill);
            BookID = source.Book.Id;
            Price = source.Price;
            Amount = source.Amount;
        }

        
    }
}