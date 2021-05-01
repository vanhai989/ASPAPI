
using CRUDApi.Data;
using CRUDApi.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Respository.Impl
{
    public class CustomerRespositoryIml : ICustomerRespository
    {
        private readonly CustomerContext _DataContext;

        public CustomerRespositoryIml(CustomerContext dataContext)
        {
            _DataContext = dataContext;
        }
        public int CountProduct()
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetCustomer(string _gender)
        {
               return Task.Run(() =>
               {
                   var result = _DataContext.Customers.Where(e => e.Gender == _gender);
                   return result.ToList();
               });
        }

        public bool IsPrime(int candidate)
        {
            if (candidate < 2)
            {
                return false;
            }
            throw new NotImplementedException("Not fully implemented.");
        }
    }
}
