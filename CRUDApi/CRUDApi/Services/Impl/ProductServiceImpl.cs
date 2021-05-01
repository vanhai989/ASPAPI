using CRUDApi.Models.ProductModes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Services.Impl
{
    public class ProductServiceImpl : IProductService
    {
        public int CountProducts()
        {
            return 0;
        }

        public Task SendEmail(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
