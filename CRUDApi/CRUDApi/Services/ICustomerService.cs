using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDApi.Models.Customers;

namespace CRUDApi.Services
{

    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers(string limit);
    }
}
