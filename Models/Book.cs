
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book 
    {
       [Key]
       public int Id {get;set;}

       [Required]
       public string Title {get;set;}
        [Required]
       public string Author {get;set;}
       public BookType Type {get;set;}

       public int CurrentAmount {get;set;}

       virtual public ICollection<BillDetail> InBillDetails {get;set;} = new List<BillDetail>();

        
    }
}