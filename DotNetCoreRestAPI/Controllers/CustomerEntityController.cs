using DotNetCoreRestAPI.Data;
using DotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DotNetCoreRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerEntityController : ControllerBase
    {
        // GET: api/CustomerEntity
        CustomerDBContext customerDBCntxt;

        public CustomerEntityController(CustomerDBContext customerDBContext)
        {
            customerDBCntxt = customerDBContext;
        }

        [HttpGet]
        [Route("Fetch")]
        public IActionResult Get()
        {
            return StatusCode(StatusCodes.Status200OK, customerDBCntxt.Customers);
        }

        // GET: api/CustomerEntity/5
        [HttpGet("{id}", Name = "FetchOne")]
        public IActionResult Get(int id)
        {
            var customer = customerDBCntxt.Customers.SingleOrDefault(c => c.Id == id);
            if (customer != null)
            {
                return StatusCode(StatusCodes.Status200OK, customer);
            }
            return StatusCode(StatusCodes.Status404NotFound, "Customer is not found");
        }
        [HttpGet]
        [Route("sort")]
        public IActionResult Get(string sortOrder)
        {
            IQueryable<Customer> customers;
            switch (sortOrder)
            {
                case "DESC":
                    customers = customerDBCntxt.Customers.OrderByDescending(p => p.Id);
                    break;
                case "ASC":
                    customers = customerDBCntxt.Customers.OrderBy(p => p.Id);
                    break;
                default:
                    customers = customerDBCntxt.Customers; break;

            }
            return StatusCode(StatusCodes.Status200OK, customers);
        }

        [HttpGet]
        [Route("paging")]
        public IActionResult Get(int? pageNo, int? pageSize)
        {
            var customers = from p in customerDBCntxt.Customers.OrderBy(p => p.Id) select p;
            int currentPage = pageNo ?? 1;
            int currentPageSize = pageSize ?? 5;
            var items = customers.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize).ToList();
            return StatusCode(StatusCodes.Status200OK, items);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult GetCustomer(string searchName)
        {
            var customer = customerDBCntxt.Customers.Where(c => c.Name.StartsWith(searchName));
            return StatusCode(StatusCodes.Status200OK, customer);
        }

        // POST: api/CustomerEntity
        [HttpPost]
        //        [Route("Add")]
        public IActionResult Post([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                customerDBCntxt.Customers.Add(customer);
                customerDBCntxt.SaveChanges(true);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // PUT: api/CustomerEntity/5
        [HttpPut]
        [Route("{id}/edit")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            try
            {
                if (id != customer.Id)
                {
                    return BadRequest();

                }
                else
                {
                    customerDBCntxt.Customers.Update(customer);
                    customerDBCntxt.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created, "Customer Deatils are updated");
                }
                //var customer = customerDBCntxt.Customers.FirstOrDefault(x => x.Id == id);
                //if (customer != null)
                //{
                //    customer.Name = name;
                //    customer.Phone = phone;
                //    customerDBCntxt.Customers.Update(customer);
                //    customerDBCntxt.SaveChanges(true);
                //    return StatusCode(StatusCodes.Status201Created, "Customer Deatils are updated");
                //}
            }
            catch (Exception e) { return NotFound("Customer is not found"); }
            //else
            //{
            //   
            //}

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //customerDBCntxt.Customers.RemoveRange(id);
            var customer = customerDBCntxt.Customers.FirstOrDefault(x => x.Id == id);
            if (customer != null)
            {
                customerDBCntxt.Customers.Remove(customer);
                customerDBCntxt.SaveChanges(true);
                return NoContent();
            }
            else
            { return NotFound("Customer not found"); }
        }
    }
}
