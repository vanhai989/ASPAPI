
using CRUDApi.Data;
using CRUDApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Respository.Impl
{
    public class CustomerRespositoryIml : ICustomerRespository
    {
        private CustomerContext _DataContext;

        public CustomerRespositoryIml(CustomerContext dataContext)
        {
            _DataContext = dataContext;
        }
        public int CountProduct()
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetProducts(string _gender)
        {
               return Task.Run(() =>
               {
                   var result = _DataContext.Customers.Where(e => e.gender == _gender);
                   return result.ToList();
               });
        }
    }
}
