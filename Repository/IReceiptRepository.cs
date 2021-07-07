using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookStore.Repository
{
    public interface IReceiptRepository
    {
         Task<List<Receipt>> GetAll();

         Task<Receipt> GetSingleReceipt(int id);      
         Task<Receipt> AddReceipt(AddReceiptActionModel model);

         Task<Receipt> EditReceipt (UpdateReceiptActionModel model);
         
         Task<EntityEntry> DeleteReceipt (int id);

          
    }
}
