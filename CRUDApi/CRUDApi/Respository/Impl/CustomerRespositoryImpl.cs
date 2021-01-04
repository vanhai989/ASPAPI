
using CRUDApi.Data;
using CRUDApi.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Respository.Impl
{
    public class CustomerRespositoryImpl : ICustomerRespository
    {
        private readonly Data.CustomerContext _DataContext;

        public CustomerRespositoryImpl(Data.CustomerContext dataContext)
        {
            _DataContext = dataContext;
        }
        public Task<List<Models.Customers.Customer>> GetCustomers(string _gender)
        {
               return Task.Run(() =>
               {
                   var result = _DataContext.Customers.Where(e => e.Gender == _gender);
                   return result.ToList();
               });
        }
    }
}
