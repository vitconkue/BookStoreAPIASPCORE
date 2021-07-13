using System;
using System.Globalization;
using BookStore.Models;

namespace BookStore.DTO
{
    public class BillDTO
    {
        public  int Id { get; set; }
        public CustomerDTO Customer {get;set;}

        public string DateTime {get;set;}

        public int Total {get;set;}

        public BillDTO(Bill source)
        {
            Id = source.BillId;
            Customer = new CustomerDTO
            (source.Customer) ;
            DateTime = source.DateTime.Date.ToString(CultureInfo.InvariantCulture);
            Total = source.Total;
        }   

    
    }
}