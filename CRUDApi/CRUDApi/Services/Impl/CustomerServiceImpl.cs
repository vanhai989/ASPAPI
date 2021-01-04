using CRUDApi.Models.Customers;
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

        public async Task<List<Customer>> GetCustomers(string limit)
        {
            return await customerRespository.GetCustomers(limit);
        }
       
    }
}
