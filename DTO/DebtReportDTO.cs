using System;
using BookStore.Models;

namespace BookStore.DTO
{
    public class DebtReportDTO
    {
        public CustomerDTO Customer {get;set;}
        public int Before {get;set;}
        
        public int ChangeAmount {get;set;}
        public int After {get;set;}

        public DateTime Date {get;set;}

        public DebtReportDTO(DebtReport source)
        {
            Customer = new CustomerDTO(source.Customer); 
            Before = source.Before;
            After = source.After;
            ChangeAmount = source.ChangeAmount;
            Date = source.Date;
        }
    }
}