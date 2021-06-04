using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Bill
    {
        [Key]
        public int BillId {get;set;}
        [Required]
        virtual public Customer Customer {get;set;}
        public DateTime DateTime { get; set; }

        virtual public ICollection<BillDetail> Details {get;set;}
    }
}