using CRUDApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(string nameProduct);
    }
}
