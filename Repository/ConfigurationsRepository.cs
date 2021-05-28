using System.Collections.Generic;
using BookStore.Models;
using BookStore.Data;
using System.Linq;

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
            return _context.Configurations.ToList();

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
    }
}