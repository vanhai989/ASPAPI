using CRUDApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Respository
{
    public interface IProductRespository
    {
        Task<List<Product>> GetProducts(string nameProduct);
    }
}
        