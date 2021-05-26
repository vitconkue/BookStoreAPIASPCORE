using System;


namespace BookStore.Errors
{
    public class NoBookFoundError : ErrorBase
    {
        public NoBookFoundError()
        {
            ErroDescription = "No book found satisfied your request";
        }
    }

}