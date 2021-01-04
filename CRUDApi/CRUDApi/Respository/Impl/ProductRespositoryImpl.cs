using CRUDApi.Data;
using CRUDApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Respository.Impl
{
    public class ProductRespositoryImpl : IProductRespository
    {
        private readonly ProductContext productContext;

        public ProductRespositoryImpl(ProductContext _productContext)
        {
            productContext = _productContext;
        }

        public Task<List<Product>> GetProducts(string nameProduct)
        {
            return Task.Run(() =>
            {
                var result = productContext.Products.Where(e => e.ProductName == nameProduct);
                return result.ToList();
            });
        }
    }
}
