using System.ComponentModel.DataAnnotations;

namespace BookStore.ActionModels
{
    public class UpdateBillEntryModel
    {

         public int newBookId {get;set;}
         public int newAmount {get;set;}

         public int newPrice {get;set;}
    }

}