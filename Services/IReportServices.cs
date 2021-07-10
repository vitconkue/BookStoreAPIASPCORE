using System.Collections.Generic;
using BookStore.Models; 

namespace BookStore.Services
{
    public interface IReportService
    {
        List<BookReport> GetBooksReportRecords(int month, int year); 

        List<DebtReport> GetDebtRecords(int month, int year);
    }
}