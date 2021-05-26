using System.ComponentModel.DataAnnotations;

namespace BookStore.ActionModels
{
    public class AddBookModel
    {
        [Required]
        public string Title; 

        public string Author;

        public int TypeID;

        public int Amount; 
    }
}