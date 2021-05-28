using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Configuration
    {
        [Key]
        public string Name {get;set;}

        [Required]
        public int Value {get;set;}
        public bool Status {get;set; }  = true; 
    }

  
}