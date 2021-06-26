﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repository
{
    public interface IReceiptRepository
    {
         Task<List<Receipt>> GetAll(); 
        
    }
}
