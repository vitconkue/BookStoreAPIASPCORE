using System;
using BookStore.Models;

namespace BookStore.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        public string Name {get;set;}

        public string Address {get;set;}

        public string PhoneNumber { get; set; }

        public string Email {get;set;}

        public int CurrentDebt {get;set;}

        public CustomerDTO(Customer source)
        {
            Id = source.Id;
            Name = source.Name;
            Address = source.Address;
            PhoneNumber = source.PhoneNumber;
            Email = source.Email; 
            CurrentDebt = source.CurrentDebt;
        }
    }

}