using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookStore.Repository
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext _context;

        public ICustomerRepository _customerRepository { get; }

        public ReceiptRepository(AppDbContext context, ICustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }
        public async Task<List<Receipt>> GetAll()
        {
            var result = await _context.Receipts
            .Include(receipt => receipt.Customer)
            .OrderByDescending(receipt => receipt.DateTime)
            .ToListAsync(); 

            return result;
            
        }

        public async Task<Receipt> AddReceipt(AddReceiptActionModel model)
        {
            var foundCustomer = await _context.Customers.FirstOrDefaultAsync(customer => customer.Id == model.CustomerId); 
            if(foundCustomer == null)
            {
                return null; 
            }

            Receipt newReceipt = new Receipt {
                Customer = foundCustomer,
                MoneyAmount = model.MoneyAmount,
                DateTime = DateTime.Now
                
            };

            // subtract debt
            foundCustomer.CurrentDebt -= model.MoneyAmount;
            _ =  await  _context.Receipts.AddAsync(newReceipt); 
            _context.SaveChanges();

            _customerRepository.RefreshCustomerDebtField(foundCustomer.Id);
            return newReceipt;
        }

        public async Task<Receipt> GetSingleReceipt(int id)
        {
            var result = await _context
            .Receipts
            .Include(receipt => receipt.Customer)
            .FirstOrDefaultAsync(receipt => receipt.ReceiptID == id); 

            return result;
        }

        public async Task<Receipt> EditReceipt(UpdateReceiptActionModel model)
        {
            Receipt found = await 
            _context.Receipts
            .Include(receipt => receipt.Customer)
            .FirstOrDefaultAsync(receipt => receipt.ReceiptID == model.ReceiptID);

            if(found == null)
                return null;
            
            found.MoneyAmount = model.MoneyAmount; 
            
        
            await _context.SaveChangesAsync(); 
             _customerRepository.RefreshCustomerDebtField(found.Customer.Id);

            return found;

        }

        public  async Task<EntityEntry> DeleteReceipt(int id)
        {
            Receipt found = await 
            _context.Receipts.Include(receipt => receipt.Customer).FirstOrDefaultAsync(receipt => receipt.ReceiptID == id);

            if(found == null)
            {
                return null;
            }
            var result =  _context.Receipts.Remove(found);

            await _context.SaveChangesAsync(); 
            
            _customerRepository.RefreshCustomerDebtField(found.Customer.Id);    
            

            return result;
        }
    }


}
