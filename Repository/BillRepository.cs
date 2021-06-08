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

        public IBookRepository _bookRepository { get; }

        public BillRepository(AppDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        public List<BillDetail> AddBookToBill(int BillId, AddBookToBill model)
        {
            var result = new List<BillDetail>();
            var foundBill = _context.Bills.FirstOrDefault(bill => bill.BillId == BillId);

            var foundBook = _context.Books.FirstOrDefault(book => book.Id == model.BookId); 
            
            if(foundBill == null || foundBook == null)
            {
                return null;
            }
            
            BillDetail newDetail = new BillDetail
            {
                Book = foundBook,
                Amount = model.Amount,
                Price = model.Price
            };
            foundBill.Details.Add(newDetail);
            
            

            // update book amount changing record
            var bookAmountChangingrecord = _bookRepository.SaveNewBookAmountChangingRecord(foundBook.Id,model.Amount,false);
            // update current book amount
            foundBook.CurrentAmount = foundBook.CurrentAmount - model.Amount;



            // update debt record

            _context.SaveChanges();


            result = foundBill.Details.ToList();
            

            return result;            


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

        public EntityEntry DeleteBillDetailEntry(int billDetailId)
        {
            var foundBillDetailEntry  = _context.BillsDetails.FirstOrDefault(entry => entry.BillDetailId == billDetailId);
            if(foundBillDetailEntry == null)
                return null;
            var result = _context.Remove(foundBillDetailEntry);

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