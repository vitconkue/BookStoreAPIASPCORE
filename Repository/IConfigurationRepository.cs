using System.Collections.Generic;
using BookStore.Models;

namespace BookStore.Repository
{
    public interface IConfigurationRepository
    {
         List<Configuration> GetAllConfigurations(); 

         Configuration ToggleConfigurationStatus(string ConfigurationName); 

         Configuration GetSingleConfiguration(string ConfigurationName); 

        
    }
}
