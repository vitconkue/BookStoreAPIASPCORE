

using System.Collections.Generic;
using System.Linq;
using BookStore.Models;
using BookStore.Repository;

namespace BookStore.Services
{
    public class ReportServices : IReportService
    {
        private readonly IBookRepository _bookRepository;

        private ICustomerRepository _customerRepository ;

        private readonly IBillRepository _billRepository;
        private readonly IReceiptRepository _receiptRepository;

        public ReportServices(IBookRepository bookRepository, 
        ICustomerRepository customerRepository, 
        IBillRepository billRepository, 
        IReceiptRepository receiptRepository)
        {
            _bookRepository  = bookRepository ;
            _customerRepository = customerRepository; 
            _billRepository = billRepository;
            _receiptRepository = receiptRepository; 
        }

        private List<BookReport> GetSingleBookMonthlyRecord(int bookId, int month, int year)
        {
            Book foundBook = _bookRepository.GetById(bookId);
            var result = new List<BookReport>(); 
            // get all from regular book amount changing record
            var allBookAmountChangingRecord = _bookRepository.GetBookAmountChangingRecordsByMonthAndBookId(bookId,month,year);
            // get all from billDetail
            var allBillDetails = _billRepository.GetBillDetailsWithSingleBookByMonth(bookId,month,year);
            // join all to a record list 
                // join from allBookAmountChangingRecord
            foreach(var bookAmountChangingRecord in allBookAmountChangingRecord )
            {
                int changed = bookAmountChangingRecord.AmountChanged;
                var newReportRecord = new BookReport 
                {
                    ChangeAmount = (bookAmountChangingRecord.IsImport) ?  changed : -changed,
                    Before =  0, 
                    After = 0,
                    Book = foundBook,
                    Date = bookAmountChangingRecord.DateChanged
                }; 

                result.Add(newReportRecord);
            }
            // join from satisfied bill detail
            foreach(var detail in allBillDetails)
            {
                int changed = detail.Amount;
                var newReportRecord = new BookReport 
                {
                    ChangeAmount = - changed,
                    Before =  0, 
                    After = 0,
                    Book = foundBook,
                    Date = detail.Bill.DateTime
                }; 

                result.Add(newReportRecord);
            }

            result = result.OrderByDescending(record => record.Date).ToList(); 
            int afterAmount = foundBook.CurrentAmount;

            foreach(var record in result)
            {
                record.After = afterAmount;
                record.Before = afterAmount - record.ChangeAmount;
                afterAmount = record.Before;
            } 

            return result;
        }
        public List<BookReport> GetBooksReportRecords(int month, int year)
        {
            var result = new List<BookReport>();
            var listBookId = _bookRepository.GetBooksIdWithChangedAmountInMonth(month,year); 
            foreach(var bookId in listBookId)
            {
                List<BookReport> toAdd = GetSingleBookMonthlyRecord(bookId,month,year); 
                result.AddRange(toAdd);
            }
            return result.OrderByDescending(record => record.Date).ToList();
        }

        public List<DebtReport> GetDebtRecords(int month, int year)
        {
            List<DebtReport> debtReports = new List<DebtReport>();
            // get all from bill details


            // get all from receipt details


            // sort by time


             
            return debtReports;
        }
    }
}