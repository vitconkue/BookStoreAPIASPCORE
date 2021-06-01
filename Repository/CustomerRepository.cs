using System.Collections.Generic;
using BookStore.Models;
using BookStore.Data;
using System.Linq;
using BookStore.ActionModels;

namespace BookStore.Repository
{
    class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public Customer AddCustomer(AddCustomerModel addCustomerModel)
        {
            Customer newCustomer = new Customer 
            {
                Name  = addCustomerModel.Name,
                Address = addCustomerModel.Address,
                PhoneNumber = addCustomerModel.PhoneNumber
            };
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return newCustomer;
            
        }

        public List<Customer> GetAllCustomers()
        {
            var resultCustomers = _context.Customers.OrderBy(customer => customer.Id).ToList();

            return resultCustomers;
        }

        public Customer GetSingleCustomer(int id)
        {
            var resultCustomer = _context.Customers.FirstOrDefault(customer => customer.Id == id); 
            return resultCustomer;
        }
    }


}