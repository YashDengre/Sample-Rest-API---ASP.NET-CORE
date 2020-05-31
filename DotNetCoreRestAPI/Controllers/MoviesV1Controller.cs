using DotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotNetCoreRestAPI.Controllers
{
    //[ApiVersion("1.0")]
    [ApiVersion("1.0", Deprecated = true)] //use this method for deprecated
    // [Route("api//movies")]   //version for query
    //[Route("api/v{version:apiVersion}/movies")]    //adding the url path versioning
    [Route("api/movies")]      //version for media type
    [ApiController]
    public class MoviesV1Controller : ControllerBase
    {
        static List<MoviesV1> movies = new List<MoviesV1>()
        {
            new  MoviesV1(){Id=1,Name = "3 Idiots"}
        };
        // GET: api/MoviesV1
        [HttpGet]
        public IEnumerable<MoviesV1> Get()
        {
            return movies;
        }

        // GET: api/MoviesV1/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MoviesV1
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/MoviesV1/5
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
