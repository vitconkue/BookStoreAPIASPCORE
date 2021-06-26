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
    }


}
