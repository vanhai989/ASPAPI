using CRUDApi.Models.CustomerModels;
using CRUDApi.Respository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDApi.Services.Impl
{
    public class CustomerServiceImpl : ICustomerService
    {
        private readonly ICustomerRespository customerRespository;

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
            return await customerRespository.GetCustomer(limit);
        }
       
    }
}
