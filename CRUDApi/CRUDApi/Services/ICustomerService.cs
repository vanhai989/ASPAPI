using CRUDApi.Data;
using CRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApi.Services
{

    public interface ICustomerService
    {
        int CountProducts();
        Task<List<Customer>> GetProducts(string limit);
    }
}
