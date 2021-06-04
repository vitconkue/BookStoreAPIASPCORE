using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookStore.Models
{
    public class BookAmountChangingRecord
    {
        [Key, Column(Order = 0)]
        public Book Book {get;set;}

        [Key, Column(Order = 1)]

        public int ChangeNumber {get;set;}

        [Required]
        public bool IsImport {get;set;}  = false;

        [Required]
        public int AmountChanged {get;set;}
        
    }
}