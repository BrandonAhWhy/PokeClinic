using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokeClinic.Models;
using PokeClinic.DataAccess;
using System.Diagnostics;



namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : Controller
    {
        
        UserRepository dbAccess;
        public UserController(UserRepository repo){
            dbAccess =  repo;
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            Console.WriteLine(dbAccess.Get(1).Name);
            return null;
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/user
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
