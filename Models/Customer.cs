using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name {get;set;}

        public string Address {get;set;}

        public string PhoneNumber { get; set; }

        public string Email {get;set;}

        
        public int CurrentDebt {get;set;} = 0 ;

        virtual public ICollection<Bill> Bills {get;set;} = new List<Bill>();

        
        virtual public ICollection<Receipt> Receipts {get;set;} = new List<Receipt>();
        
    }
}