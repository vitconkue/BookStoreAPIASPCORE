using System;
using BookStore.Models;

namespace BookStore.DTO
{
    public class BillDTO
    {
        public  int Id { get; set; }
        public Customer Customer {get;set;}

        public string DateTime {get;set;}

        public BillDTO(Bill source)
        {
            Id = source.BillId;
            Customer = source.Customer;
            DateTime = source.DateTime.Date.ToString();
        }

    
    }
}