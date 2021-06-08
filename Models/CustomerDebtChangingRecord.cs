using System.ComponentModel.DataAnnotations;
using System;


namespace BookStore.Models
{
    public class CustomerDebtChangingRecord
    {

        [Key]
        public int RecordId {get;set;}
        public Customer Book {get;set;}

       

        [Required]
        public bool IsCollectMoney {get;set;}  = false;

        [Required]
        public int BeforeChangeAmount {get;set;}

        public int ChangeMoneyAmount {get;set;}

        public DateTime DateChanged{get;set;} = DateTime.Now;
            
    }
}