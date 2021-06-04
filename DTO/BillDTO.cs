using System;
using BookStore.Models;

namespace BookStore.DTO
{
    public class BillDTO
    {
        public  int Id { get; set; }
        public Customer Customer {get;set;}

        public DateTime DateTime {get;set;}

        static public object  GetDTO(Bill Bill)
        {
            return new  {
                Id = Bill.BillId,
                Customer = Bill.Customer,
                DateTime = Bill.DateTime.Date.ToString()
            };
        }

    }
}