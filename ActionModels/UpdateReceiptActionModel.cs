using System.ComponentModel.DataAnnotations;


public class UpdateReceiptActionModel
{
    public int ReceiptID {get;set;}
    public int CustomerID {get;set;}
    public int MoneyAmount {get;set;}
}