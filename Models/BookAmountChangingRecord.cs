using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace BookStore.Models
{
    public class BookAmountChangingRecord
    {

        [Key]
        public int RecordId {get;set;}
        public Book Book {get;set;}

       

        [Required]
        public bool IsImport {get;set;}  = false;

        [Required]
        public int AmountBeforeChanged {get;set;}

        public int AmountChanged {get;set;}

        public DateTime DateChanged{get;set;} = DateTime.Now;

            
    }
}