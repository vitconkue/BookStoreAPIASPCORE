namespace BookStore.ActionModels
{
    public class BulkUpdateConfigurationActionModel
    {
        public int MaximumAmountBookLeftBeforeImport {get;set;}
        public int MaximumDebtCustomer {get;set;}
        public int MinimumAmountBookLeftAfterSelling {get;set;}
        public int MinimumImportBook {get;set;}  
    }
}