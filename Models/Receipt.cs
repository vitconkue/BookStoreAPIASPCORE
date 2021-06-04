using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Receipt
    {
        [Key]
        public int ReceiptID {get;set;}
        public Customer Customer {get;set;}
        public int MoneyAmount {get;set;}

        public DateTime DateTime {get;set;}
    }
}