using System.Collections.Generic;
using BookStore.Models;
using BookStore.ActionModels;

namespace BookStore.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();

        Customer GetSingleCustomer(int id); 

        Customer AddCustomer(AddCustomerModel addCustomerModel);

        int? CalculateTotalDebt(int CustomerId);
    }
}