using System;
using BookStore.Models;


namespace BookStore.DTO
{
    public class ReceiptDTO
    {
        public int ReceiptID {get;set;}
        public CustomerDTO Customer {get;set;}
        public int MoneyAmount {get;set;}

        public DateTime DateTime {get;set;}
    }
}