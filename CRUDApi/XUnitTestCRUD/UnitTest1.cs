using CRUDApi.Data;
using CRUDApi.Models.CustomerModels;
using CRUDApi.Respository.Impl;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestCRUD
{
    public class UnitTest1
    {
        private readonly CustomerContext _DataContext;
        List<Customer> _stringEqual = new List<Customer>();

       [Fact]
        public void MyTest()
        {
            _stringEqual.Add(new Customer() {Gender = "Male" });
            var TestRPSTR = new CustomerRespositoryIml(_DataContext);

            var result = TestRPSTR.GetCustomer("Male");
            var isExist = result.Result.Find(x => x.Gender == "Male");

           // Assert.Contains(isExist);

        }
    }
}
