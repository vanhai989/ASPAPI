using CRUDApi.Models.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDApi.Respository
{
    public interface ICustomerRespository
    {
        Task<List<Customer>> GetCustomers(string _gender);
    }
}
