using System.ComponentModel.DataAnnotations;

namespace BookStore.ActionModels
{
    public class AddBookModel
    {
        [Required]
        public string Title {get;set;}

        public string Author {get;set;}

        public int TypeID {get;set;}

        public int Amount {get;set;}
    }
}