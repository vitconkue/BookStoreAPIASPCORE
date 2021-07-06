using BookStore.ActionModels;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BookStore.Repository
{
    public interface IBillRepository
    {
        List<Bill> GetAllBills(); 

        EntityEntry DeleteBill(int id);
        Bill GetSingleBill(int id);

        Bill CreateBill(AddBillModel model); 
        BillDetail UpdateSingleBillEntry(int billDetailID,UpdateBillEntryModel model);

        List<BillDetail> GetSingleBillDetail(int id);

        List<BillDetail> AddBookToBill(int BillId, AddBookToBill model);

        List<BillDetail> BulkAddBookToBill(int BillId, List<AddBookToBill> model);

        EntityEntry DeleteBillDetailEntry(int billDetailId);
    }

}