using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BillDetail
    {
        [Key]
        public int BillDetailId { get; set; }
        virtual public Book Book {get;set;}

        public int Price {get;set;}

        public int Amount {get;set;}


        virtual public Bill Bill {get;set;}

        
    }


}