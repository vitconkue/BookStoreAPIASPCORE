using System.ComponentModel.DataAnnotations;

namespace BookStore.ActionModels
{
    public class AddCustomerModel
    {
       [Required]
        public string Name {get;set;}

        public string Address {get;set;}

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}