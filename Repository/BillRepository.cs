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

        private IBookRepository _bookRepository { get; }

        private readonly ICustomerRepository _customerRepository;

        public BillRepository(AppDbContext context, IBookRepository bookRepository, ICustomerRepository customerRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _customerRepository = customerRepository;
        }

        public List<BillDetail> AddBookToBill(int BillId, AddBookToBill model)
        {
            var result = new List<BillDetail>();

            var foundBill = _context.Bills
            .Include(bill => bill.Customer)
            .FirstOrDefault(bill => bill.BillId == BillId);

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
            Bill found = _context.Bills
            .Include(bill => bill.Customer)
            .Include(bill => bill.Details)
            .FirstOrDefault(bill => bill.BillId == id);
            if(found == null)
            {    
                return null;
            }
            var result = _context.Remove(found); 
            _context.SaveChanges();

            _customerRepository.RefreshCustomerDebtField(found.Customer.Id); 


            return result;
        }

        public EntityEntry DeleteBillDetailEntry(int billDetailId)
        {
            var foundBillDetailEntry  = _context.BillsDetails
            .Include(billDetail => billDetail.Bill)
            .ThenInclude(bill => bill.Customer).FirstOrDefault(entry => entry.BillDetailId == billDetailId);
            if(foundBillDetailEntry == null)
                return null;
            

            var result = _context.Remove(foundBillDetailEntry);

            _context.SaveChanges();
            _customerRepository.RefreshCustomerDebtField(foundBillDetailEntry.Bill.Customer.Id); 


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
            .Include(bill =>bill.Customer)
            .FirstOrDefault(bill => bill.BillId == id); 
            if(found == null)
                return null;
            
            var result = found.Details.ToList();

            
            return result;
        }

        public BillDetail UpdateSingleBillEntry(int billDetailID,UpdateBillEntryModel model)
        {
            BillDetail foundBillDetail = _context.BillsDetails
             .Include(billDetail => billDetail.Bill)  
            .ThenInclude(bill => bill.Customer)
            .FirstOrDefault(billDetail => billDetail.BillDetailId == billDetailID);

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


            _context.SaveChanges();

            _customerRepository.RefreshCustomerDebtField(foundBillDetail.Bill.Customer.Id);
            return foundBillDetail;                  

        
        }

        public List<BillDetail> BulkAddBookToBill(int BillId, List<AddBookToBill> model)
        {
            var result = new List<BillDetail>();
            
            var foundBill = _context.Bills
            .Include(bill => bill.Customer)
            .FirstOrDefault(bill => bill.BillId == BillId);

            List<int> BookIdToAdd = model.Select(model => model.BookId).ToList(); 

            List<Book> BookToAdd = _context.Books.Where(book => BookIdToAdd.Contains( book.Id)).ToList();
            
            // create new bill details

            List<BillDetail> newBillDetails = new List<BillDetail>(); 

            int count = BookToAdd.Count;
            for(int i = 0;  i < count; ++i)
            {
                newBillDetails.Add(
                    new BillDetail {
                        Book = BookToAdd[i],
                        Amount = model[i].Amount,
                        Price = model[i].Price,
                        Bill = foundBill,
                    }
                );

                BookToAdd[i].CurrentAmount -= model[i].Amount;
            }

             _context.BillsDetails.AddRange(newBillDetails);
            _context.SaveChanges();

            _customerRepository.RefreshCustomerDebtField(foundBill.Customer.Id);

            return newBillDetails;
        }

        // report part
        public List<BillDetail> GetBillDetailsWithSingleBookByMonth(int bookId, int month, int year)
        {
            List<BillDetail> billDetails = new List<BillDetail>();
            billDetails = _context
                .BillsDetails
                .Include(detail => detail.Bill).ThenInclude(bill => bill.Customer)
                .Include(detail => detail.Book)
                .Where(detail => detail.Book.Id == bookId)
                .Where(detail => detail.Bill.DateTime.Month == month && detail.Bill.DateTime.Year == year )
                .OrderByDescending(detail => detail.Bill.DateTime)
                .ToList(); 

            return billDetails; 
        }
    }
}