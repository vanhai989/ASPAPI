using CRUDApi.Models.CustomerModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDApi.Respository
{
    public interface ICustomerRespository
    {
        int CountProduct();

        Task<List<Customer>> GetCustomer(string _gender);
    }
}
