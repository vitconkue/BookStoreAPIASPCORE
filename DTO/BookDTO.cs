using BookStore.Models;

namespace BookStore.DTO
{
    public class BookDTO{
        public int Id {get;set;}

       public string Title {get;set;}
       public string Author {get;set;}
       public BookType Type {get;set;}

       public int CurrentAmount {get;set;}

       public BookDTO(Book source)
       {
           Id = source.Id;
           Title = source.Title;
           Author = source.Author;

           Type = source.Type;

           CurrentAmount = source.CurrentAmount;
       }
    }
}