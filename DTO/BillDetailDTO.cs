using System;
using BookStore.Models;

namespace BookStore.DTO
{
    public class BillDetailDTO
    {

        public int BillDetailId {get;set;}

        public int BookID {get;set;}

        public int Price {get;set;}
        public int Amount {get;set;}

        public BillDetailDTO(BillDetail source)
        {
            BillDetailId = source.BillDetailId; 
            BookID = source.Book.Id;
            Price = source.Price;
            Amount = source.Amount;
        }

        
    }
}