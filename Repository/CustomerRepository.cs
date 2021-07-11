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
            var customer = _context.Customers
            .Include(customer => customer.Bills)
            .ThenInclude(bill => bill.Details)
            .ThenInclude(billDetail => billDetail.Book)
            .Include(customer => customer.Receipts)
            .FirstOrDefault(customer => customer.Id == CustomerId);
            if(customer == null)
            {
                return null; 
            }
            int sumAllBill = customer.Bills.ToList().Sum(bill => bill.Total); 
             
            // TODO: Sum all receipt
            int sumAllReceipt = customer.Receipts.ToList().Sum(receipt => receipt.MoneyAmount );

            return sumAllBill - sumAllReceipt; 
        }

        public void RefreshCustomerDebtField(int customerId)
        {
            Customer found = _context.Customers.FirstOrDefault(customer => customer.Id == customerId); 
            if(found == null)
                return;
            int? newDebt = CalculateTotalDebt(customerId); 
            
            if(newDebt == null)
                return;
            
            found.CurrentDebt = newDebt.Value;
            _context.SaveChanges();
            
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

        public Customer UpdateCustomer(UpdateCustomerActionModel updateCustomerActionModel)
        {
            var found = _context.Customers.FirstOrDefault(customer => customer.Id == updateCustomerActionModel.CustomerID); 
            if(found == null)
                return null;
            
            found.Address = updateCustomerActionModel.Address;
            found.Email =updateCustomerActionModel.Email;
            found.Name = updateCustomerActionModel.Name;
            found.PhoneNumber = updateCustomerActionModel.PhoneNumber;
            _context.SaveChanges();
            return found;
        }
    }


}