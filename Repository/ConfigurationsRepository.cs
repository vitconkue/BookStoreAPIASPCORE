using System.Collections.Generic;
using BookStore.Models;
using BookStore.Data;
using System.Linq;
using BookStore.ActionModels;

namespace BookStore.Repository
{
    public class ConfigurationsRepository : IConfigurationRepository
    {
        private readonly AppDbContext _context;

        public ConfigurationsRepository(AppDbContext context)
        {
            _context = context; 
        }
        public List<Configuration> GetAllConfigurations()
        {
            return _context.Configurations.OrderBy(con => con.Name).ToList();

        }

        public Configuration GetSingleConfiguration(string ConfigurationName)
        {
            Configuration result = _context.Configurations.FirstOrDefault(c => c.Name == ConfigurationName); 

            return result; 
        }

        public Configuration ToggleConfigurationStatus(string ConfigurationName)
        {
            Configuration found = GetSingleConfiguration(ConfigurationName);
            if(found == null)
            {
                return null;
            }
            else
            {
                found.Status = ! found.Status; 
                _context.SaveChanges();
                return found;  
            }

        }

        public Configuration ChangeConfiguration(string ConfigurationName, int value)
        {
            Configuration found = GetSingleConfiguration(ConfigurationName);
            if(found == null)
            {
                return null;
            }
            found.Value = value; 

            _context.SaveChanges();
            return found;

        }

        public List<Configuration> BulkUpdateConfigurations(BulkUpdateConfigurationActionModel actionModel)
        {
            List<Configuration> result = new List<Configuration>(); 
            Configuration MinimumImportBook = GetSingleConfiguration("MinimumImportBook"); 

            MinimumImportBook.Value = actionModel.MinimumImportBook; 
            
            Configuration MaximumAmountBookLeftBeforeImport = GetSingleConfiguration("MaximumAmountBookLeftBeforeImport"); 

            MaximumAmountBookLeftBeforeImport.Value = actionModel.MaximumAmountBookLeftBeforeImport; 


            Configuration MaximumDebtCustomer = GetSingleConfiguration("MaximumDebtCustomer"); 

            MaximumDebtCustomer.Value = actionModel.MaximumDebtCustomer;

            Configuration MinimumAmountBookLeftAfterSelling = GetSingleConfiguration("MinimumAmountBookLeftAfterSelling"); 

            MinimumAmountBookLeftAfterSelling.Value = actionModel.MinimumAmountBookLeftAfterSelling;

            _context.SaveChanges();

            result.AddRange(new List<Configuration> 
            {MinimumImportBook,
            MaximumAmountBookLeftBeforeImport,
            MaximumDebtCustomer,
            MinimumAmountBookLeftAfterSelling}); 
            
            return result;
        }
    }
}