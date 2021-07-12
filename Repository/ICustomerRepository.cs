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

        Customer UpdateCustomer(UpdateCustomerActionModel updateCustomerActionModel); 

        int? CalculateTotalDebt(int CustomerId);

        void RefreshCustomerDebtField(int customerId);

        // for report
        
        List<int> GetCustomersIdWithChangedDebtInMonth(int month, int year);
    }
}