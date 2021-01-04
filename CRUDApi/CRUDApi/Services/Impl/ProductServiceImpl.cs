using CRUDApi.Data;
using CRUDApi.Models.Products;
using CRUDApi.Respository;
using CRUDApi.Respository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Services.Impl
{
    public class ProductServiceImpl : IProductService
    {
        private readonly Respository.IProductRespository productRespository;

        public ProductServiceImpl(Respository.IProductRespository _productRespositoryIml)
        {
            productRespository = _productRespositoryIml;
        }
        public async Task<List<Product>> GetProducts(string nameProduct)
        {
            return await productRespository.GetProducts(nameProduct);
        }
    }
}
