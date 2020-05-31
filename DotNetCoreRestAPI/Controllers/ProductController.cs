using DotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DotNetCoreRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        static List<Product> products = new List<Product>()
        {
            new Product() { Id = 0,ProductName = "Laptop",Price = "200"},
            new Product() { Id = 1,ProductName = "Mobile",Price = "100"}
        };

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()      //Ienumerable t Iaction Result for better return code
        {
            //return Ok(products);
            return StatusCode(StatusCodes.Status200OK, products);
        }
        // PUT: api/Products/5
        [HttpPut]
        [Route("{id}")]
        public IActionResult PutProduct([FromRoute]int id, [FromBody]Product _product)
        {
            try { products[id] = _product; }
            catch (Exception e) { }
            return StatusCode(StatusCodes.Status201Created);
        }

        // POST : api/Products
        [HttpPost, MapToApiVersion("2.0")]
        public IActionResult PostProduct(Product _product)
        {
            if (ModelState.IsValid)
            {
                products.Add(_product);
                return StatusCode(StatusCodes.Status201Created);

            }
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        }
        // DELETE: api/Products/5
        [HttpDelete]
        public IActionResult DeleteProduct([FromRoute]int id)
        {
            products.RemoveAt(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpGet("getproduct")]
        public Product GetProduct([FromRoute]int id)
        {
            return products[id];
        }
        [Route("{id}")]
        public Product GetProductby(int id)
        {
            return products[id];
        }
    }
}