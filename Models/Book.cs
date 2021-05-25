
namespace BookStore.Models
{
    public class Book 
    {
       public int Id {get;set;}
       public string Title {get;set;}

       public string Author {get;set;}
       public BookType Type {get;set;}

       public int CurrentAmount {get;set;}
        
    }
}