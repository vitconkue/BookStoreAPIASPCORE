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
            var foundBill = _context.Bills.Include(bill => bill.Customer).FirstOrDefault(bill => bill.BillId == BillId);

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
            // update current book amount
            foundBook.CurrentAmount = foundBook.CurrentAmount - model.Amount;
            // update customer debt
            var currentCustomer = foundBill.Customer; 
            currentCustomer.CurrentDebt += model.Amount * model.Price;
            
            result = foundBill.Details.ToList();
            _context.SaveChanges();
            


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
            Bill found = _context.Bills
            .Include(bill => bill.Customer)
            .Include(bill => bill.Details)
            .FirstOrDefault(bill => bill.BillId == id);
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

        public BillDetail UpdateSingleBillEntry(UpdateBillEntryModel model)
        {
            BillDetail foundBillDetail = _context.BillsDetails
            // .Include(billDetail => billDetail.Bill)  
            // .ThenInclude(bill => bill.Customer)
            .FirstOrDefault(billDetail => billDetail.BillDetailId == model.BillDetailId);

            if(foundBillDetail == null)
                return null;
          
            // save
            
                // is changed book check point
            Book foundBook = _context.Books.FirstOrDefault(book => book.Id == model.newBookId); 
            if(foundBook == null)
            {
                return null; 
            }

            int oldBillAmount = foundBillDetail.Amount;
            
            int oldBillPrice = foundBillDetail.Price;

            int oldBookAmount = foundBook.CurrentAmount; 

            //int oldCustomerDebt = foundBillDetail.Bill.Customer.CurrentDebt;

            int oldCustomerDebt  =0 ; 
                // amount checkpoint
            if(model.newAmount <= 0) 
                return null; 

                // price checkpoint
            if(model.newPrice  < 0 )
                return null;

            foundBillDetail.Book = foundBook;
            foundBillDetail.Amount = model.newAmount;
            foundBillDetail.Price = model.newPrice;


            // save new book amount and customer debt
            int newBookAmount = 0 ; 

            if(model.newAmount > oldBillAmount)
            {
                newBookAmount = oldBookAmount - (model.newAmount - oldBillAmount); 
            }

            else{
                newBookAmount = oldBookAmount + (oldBillAmount - model.newAmount ); 
            }
            foundBook.CurrentAmount = newBookAmount;

            int newCustomerDebt = 0 ; 
            if(model.newPrice * model.newAmount > oldBillAmount * oldBillPrice)
            {
                newCustomerDebt = oldCustomerDebt + (model.newPrice * model.newAmount - oldBillAmount * oldBillPrice);
            }
            else{
                newCustomerDebt = oldCustomerDebt - (oldBillAmount * oldBillPrice - model.newPrice * model.newAmount );

            }
                
            //foundBillDetail.Bill.Customer.CurrentDebt = newCustomerDebt;


            _context.SaveChanges();
            return foundBillDetail;                  

        
        }
    }
}