using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext _context;

        public ReceiptRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Receipt>> GetAll()
        {
            var result = await _context.Receipts.OrderByDescending(receipt => receipt.DateTime).ToListAsync(); 

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
                
            }   ;

            // subtract debt
            foundCustomer.CurrentDebt -= model.MoneyAmount;
            _ =  await  _context.Receipts.AddAsync(newReceipt); 

            return newReceipt;
        }
    }


}
