using BookStore.Data;
using BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BookStore.ActionModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace BookStore.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly AppDbContext _context;

        public BillRepository(AppDbContext context)
        {
            _context = context;
        }
        public Bill CreateBill()
        {
            throw new System.NotImplementedException();
        }

        public EntityEntry DeleteBill(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Bill> GetAllBills()
        {
            return _context.Bills.OrderByDescending(bill => bill.DateTime).Include(bill => bill.Customer).ToList(); 
        }

        public Bill GetSingleBill(int id)
        {
            Bill found = _context.Bills.Include(bill => bill.Customer).FirstOrDefault(bill => bill.BillId == id);
            if(found == null)
                return null;

            return found;
        }

        public List<BillDetail> GetSingleBillDetail(int id)
        {
            Bill found = _context.Bills
            .Include(bill => bill.Details)
            .ThenInclude(detail => detail.Book)
            .FirstOrDefault(bill => bill.BillId == id); 
            if(found == null)
                return null;
            
            var result = found.Details.ToList();

            
            return result;
        }

        public Bill UpdateBill()
        {
            throw new System.NotImplementedException();
        }
    }
}