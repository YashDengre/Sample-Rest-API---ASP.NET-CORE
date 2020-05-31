using DotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotNetCoreRestAPI.Controllers
{
    [ApiVersionNeutral]  //for opting out the api version for this particular api
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        // GET: api/Customer
        static List<Customer> customers = new List<Customer>()
        { new Customer() { Id = 1,Name = "Tom Cruise",Email = "tomcruise@gmail.com",Phone = "3322"},
            new Customer() { Id = 1,Name = "Robert Downy Jr",Email = "robert@gmail.com",Phone = "326"},
            new Customer() { Id = 1,Name = "Chris patt",Email = "cpatt@hotmail.com",Phone = "659"},};
        // GET: api/Customers
        public IEnumerable<Customer> Get()
        { return customers; }
        // GET: api/Customers/5
        public string Get(int id) { return "value"; }
        // POST: api/Customers
        public IActionResult Post([FromBody]Customer customer)
        {
            if (ModelState.IsValid)
            {
                customers.Add(customer);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
