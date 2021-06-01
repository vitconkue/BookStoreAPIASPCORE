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

        
        public int CurrentDebt {get;set;} = 0 ;
    }
}