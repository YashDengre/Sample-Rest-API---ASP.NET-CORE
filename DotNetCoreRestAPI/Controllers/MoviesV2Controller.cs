using DotNetCoreRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DotNetCoreRestAPI.Controllers
{
    [ApiVersion("2.0")]
    // [Route("api//movies")]   //version for query
    //[Route("api/v{version:apiVersion}/movies")]    //adding the url path versioning
    [Route("api/movies")]      //version for media type
    [ApiController]
    public class MoviesV2Controller : ControllerBase
    {
        static List<MoviesV2> movies = new List<MoviesV2>()
        {
            new  MoviesV2(){Id=1,Name = "3 Idiots",StarCast ="AK, SJ, RM"}
        };
        // GET: api/MoviesV2
        [HttpGet]
        public IEnumerable<MoviesV2> Get()
        {
            return movies;
        }

        // GET: api/MoviesV2/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MoviesV2
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/MoviesV2/5
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
