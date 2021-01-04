using CRUDApi.Controllers;
using CRUDApi.Data;
using CRUDApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CRUDApi.Enum;
using CRUDApi.Models.Products;
using IProductService = CRUDApi.Services.IProductService;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore;
using CRUDApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Models.Customers;

namespace XUnitTestProjectCRUD
{
    public class UnitTest1
    {
        private readonly CRUDApi.Data.CustomerContext _CustomerContext;
        private readonly ProductContext productContext;
        private readonly HttpClient _client;
        private readonly TestServer _server;
        private CustomersController controler;
        private Customer customerItem;

        Gender male;

        public UnitTest1()
        {
            /* var server = new TestServer(WebHost.CreateDefaultBuilder()
                 .UseStartup<Startup>()
                 .ConfigureServices(s => s.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("TestDb"))));

             var client = server.CreateClient();

             client.DefaultRequestHeaders.Clear();
             client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));

             _client = client;
             _server = server;*/
            male = Gender.Male;
            var mockRepo = new Mock<ICustomerService>();
            mockRepo.Setup(res => res.GetCustomers(male.ToString()));
            controler = new CustomersController(_CustomerContext, mockRepo.Object);

            customerItem = new Customer()
            {
                CustomerName = "Van_Hai",
                CustomerAddress = "CustomerAddress",
                CustomerImage = "CustomerImage",
                Gender = "Male",
                OrderDate = "OrderDate",
                CustomerPhone = "CustomerPhone",
                CustomerEmail = "CustomerEmail",
                Status = 1
            };
        }

        // test customerController
        // test get customer method
        [Fact]
        public async Task<List<Customer>> TestGetCustomer()
        {
            var result = await controler.GetCustomers(male.ToString());
            return result.Value;
        }

        [Fact]
        // test Put customer method
        public async Task TestPutController()
        {
            var result = await controler.PutCustomer(101, customerItem);

            bool Modified = false;

            string oldCustomer = _CustomerContext.Customers.Find(101).CustomerName.ToString();
            if (oldCustomer == "Van_Hai")
            {
                Modified = true;
            }
            Assert.True(Modified);
        }
        // test Post customer method
        [Fact]
        public async Task TestPotsController()
        {
            int count = await _CustomerContext.Customers.CountAsync();
            bool IsCreateRecord = false;
            await controler.PostCustomer(customerItem);
            if(count < await _CustomerContext.Customers.CountAsync())
            {
                IsCreateRecord = true;
            }
            Assert.True(IsCreateRecord);
        }  
    }
}



