using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDApi.Models.CustomerModels;

namespace CRUDApi.Services
{

    public interface ICustomerService
    {
        int CountProducts();
        Task<List<Customer>> GetProducts(string limit);
    }
}
