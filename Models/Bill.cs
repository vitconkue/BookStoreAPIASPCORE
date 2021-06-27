using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BookStore.Models
{
    public class Bill
    {
        [Key]
        public int BillId {get;set;}
        [Required]
        virtual public Customer Customer {get;set;}
        public DateTime DateTime { get; set; } = DateTime.Now;

        virtual public ICollection<BillDetail> Details {get;set;} = new List<BillDetail>();
        public int Total {
            get {
                return Details.ToList().Sum(detail => (detail.Price * detail.Amount));
            }
        }

    }
}