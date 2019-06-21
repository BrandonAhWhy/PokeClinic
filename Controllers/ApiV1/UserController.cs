using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PokeClinic.Models;

namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : Controller
    {
        // GET api/user
        [HttpGet]
        public ActionResult Get()
        {
            // For testing if you don't have a db setup
            //return Json(new List<User> {new User { Id = 1, Name = "Bridge", Email = "lol@no.co", Password = "******" },});
            return Json(Models.User.GetAll(25, 0));
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Json(Models.User.Get(id));
        }

        // POST api/user
        [HttpPost]
        public ActionResult<string> Post([FromBody] string value)
        {
            return "TODO";
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] string value)
        {
            return "TODO";
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            return "TODO";
        }
    }
}
