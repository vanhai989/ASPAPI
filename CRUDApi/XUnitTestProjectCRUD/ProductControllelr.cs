using CRUDApi.Controllers;
using CRUDApi.Data;
using CRUDApi.Models.Products;
using CRUDApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProjectCRUD
{
    public class ProductControllelr
    {
        private readonly ProductContext product;
        // test productController
        [Fact]
        public async Task<List<Product>> GetProducts()
        {
            var mockRepo = new Mock<IProductService>();
            mockRepo.Setup(repo => repo.GetProducts("zoro"));
            var productController = new ProductsController(product, mockRepo.Object);
            var result = await productController.GetProductByNameProduct("zoro");
            return result.Value;
        }
    }
}
