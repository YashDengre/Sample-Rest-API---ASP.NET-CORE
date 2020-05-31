using DotNetCoreRestAPI.Data;
using DotNetCoreRestAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCoreRestAPI.Services
{
    public class CustomerRepository : ICustomer
    {

        CustomerDBContext customerDBCntxt;

        public CustomerRepository(CustomerDBContext customerDBContext)
        {
            customerDBCntxt = customerDBContext;
        }

        public void AddCustomer(Customer customer)
        {
            customerDBCntxt.Customers.Add(customer);
            customerDBCntxt.SaveChanges(true);
        }

        public void DeleteCustomer(int id)
        {
            //customerDBCntxt.Customers.RemoveRange(id);
            var customer = customerDBCntxt.Customers.ToList().Find(x => x.Id == id);
            //if (customer != null)
            //{
            customerDBCntxt.Customers.Remove(customer);
            customerDBCntxt.SaveChanges(true);

            //}
            //if we use find instead for firstor default we dont need to use if condition
        }

        public IEnumerable<Customer> GetCusomters()
        {

            return customerDBCntxt.Customers;
        }

        public Customer GetCustomer(int id)
        {
            var customer = customerDBCntxt.Customers.SingleOrDefault(c => c.Id == id);
            return customer;
        }

        public void UpdateCustomer(Customer customer)
        {
            customerDBCntxt.Customers.Update(customer);
            customerDBCntxt.SaveChanges();
        }
    }
}
