using System.Collections.Generic;
using BookStore.Models;
using BookStore.Data;
using System.Linq;
using BookStore.ActionModels;
using Microsoft.EntityFrameworkCore;


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

        public int? CalculateTotalDebt(int CustomerId)
        {
            var x = _context.Customers
            .Include(customer => customer.Bills)
            .ThenInclude(bill => bill.Details)
            .ThenInclude(billDetail => billDetail.Book)
            .FirstOrDefault(customer => customer.Id == CustomerId);
            if(x == null)
            {
                return null; 
            }
            int sumAllBill = x.Bills.ToList().Sum(bill => bill.Total); 
             
            // TODO: Sum all receipt
            int sumAllReceipt = 0 ; 
            return sumAllBill - sumAllReceipt; 
        }

        public List<Customer> GetAllCustomers()
        {
            var resultCustomers = _context.Customers.OrderBy(customer => customer.Id).ToList();

            return resultCustomers;
        }

        public Customer GetSingleCustomer(int id)
        {
            var found = _context.Customers.FirstOrDefault(customer => customer.Id == id); 
            if(found == null)
                return null;
            return found;
        }
    }


}