using System.Collections.Generic;
using BookStore.Models;
using BookStore.ActionModels;

namespace BookStore.Repository
{
    public interface IConfigurationRepository
    {
         List<Configuration> GetAllConfigurations(); 

         Configuration ToggleConfigurationStatus(string ConfigurationName); 

         Configuration GetSingleConfiguration(string ConfigurationName); 

         Configuration ChangeConfiguration(string ConfigurationName, int value);

         List<Configuration> BulkUpdateConfigurations(BulkUpdateConfigurationActionModel actionModel);
    }
}
