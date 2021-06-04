using BookStore.ActionModels;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace BookStore.Repository
{
    public interface IBillRepository
    {
        List<Bill> GetAllBills(); 

        Bill GetSingleBill(int id);

        Bill CreateBill(AddBillModel model); 

        Bill UpdateBill();
        
        EntityEntry DeleteBill(int id);

        List<BillDetail> GetSingleBillDetail(int id);

        List<BillDetail> AddBookToBill(AddBookToBill model);
    }

}