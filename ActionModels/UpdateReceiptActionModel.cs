using System.ComponentModel.DataAnnotations;


public class UpdateReceiptActionModel
{
    public int ReceiptID {get;set;}
    public int CustomerId {get;set;}
    public int MoneyAmount {get;set;}
}