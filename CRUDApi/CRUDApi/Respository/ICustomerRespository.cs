using CRUDApi.Data;
using CRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Respository
{
    public interface ICustomerRespository
    {
        int CountProduct();

        Task<List<Customer>> GetProducts(string _gender);
    }
}
