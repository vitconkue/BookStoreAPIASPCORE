

using System.ComponentModel.DataAnnotations;

namespace BookStore.ActionModels
{
    public class UpdateBookActionModel
    {
        [Required]
        public int Id {get;set;}

        [MinLength(3)]
         public string Title {get;set;}
        [MinLength(3)]
        public string Author {get;set;}

        public int TypeID {get;set;}

        public int Amount {get;set;}
    }
}