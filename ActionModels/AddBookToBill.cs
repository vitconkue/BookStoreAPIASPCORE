

namespace BookStore.ActionModels
{
    public class AddBookToBill
    {
        public int BillId { get; set; }
        public int BookId { get; set; }

        public int Amount {get;set;}

        public int Price {get;set;}
    }
}