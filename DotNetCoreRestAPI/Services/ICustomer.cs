using DotNetCoreRestAPI.Models;
using System.Collections.Generic;

namespace DotNetCoreRestAPI.Services
{
    public interface ICustomer
    {
        //Crud Operations
        IEnumerable<Customer> GetCusomters();

        Customer GetCustomer(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);


    }
}
