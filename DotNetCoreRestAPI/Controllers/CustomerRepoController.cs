using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreRestAPI.Models;
using DotNetCoreRestAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreRestAPI.Controllers
{
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRepoController : ControllerBase
    {
        private ICustomer customerRepository;
        public CustomerRepoController(ICustomer customerRepositoryConst)
        {
            customerRepository = customerRepositoryConst;
        }
        // GET: api/CustomerRepo
        [HttpGet]
        [Route("Fetch")]
        public IActionResult Get()
        {
            return Ok(customerRepository.GetCusomters());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = customerRepository.GetCustomer(id);
            if (customer != null)
            {
                return StatusCode(StatusCodes.Status200OK, customer);
            }
            return StatusCode(StatusCodes.Status404NotFound, "Customer is not found");
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
                customerRepository.AddCustomer(customer);
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
                    customerRepository.UpdateCustomer(customer);
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
            customerRepository.DeleteCustomer(id);
            { return NotFound("Customer not found"); }
        }
    }
}
