using CRUDApi.Data;
using CRUDApi.Models;
using CRUDApi.Respository;
using CRUDApi.Respository.Impl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Services.Impl
{
    public class CustomerServiceImpl : ICustomerService
    {
        private ICustomerRespository customerRespository;

        public CustomerServiceImpl(ICustomerRespository _customerRespository)
        {
            customerRespository = _customerRespository;
        }
        public int CountProducts()
        {
            return 1;
        }

        public async Task<List<Customer>> GetProducts(string limit)
        {
            return await customerRespository.GetProducts(limit);
        }
       
    }
}
