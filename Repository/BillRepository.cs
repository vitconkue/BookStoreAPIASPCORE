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

        public List<BillDetail> AddBookToBill(AddBookToBill model)
        {
            throw new NotImplementedException();
        }

        public Bill CreateBill(AddBillModel model)
        {
            var foundCustomer = _context.Customers.FirstOrDefault(Customer=> Customer.Id == model.CustomerID);
            Bill newBill = new Bill {
                DateTime = DateTime.Now
            };
            newBill.Customer = foundCustomer; 

            _context.Add(newBill);
            _context.SaveChanges();
            return newBill;
        }

        public EntityEntry DeleteBill(int id)
        {
            Bill found = _context.Bills.Include(bill => bill.Customer).FirstOrDefault(bill => bill.BillId == id);
            if(found == null)
            {    
                return null;
            }
            var result = _context.Remove(found); 

            _context.SaveChanges(); 

            return result;
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