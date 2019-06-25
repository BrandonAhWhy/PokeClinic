using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PokeClinic.Models;
using System.Collections.Specialized;

namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : Controller
    {
        // GET api/user
        [HttpGet]
        public ActionResult GetAll()
        {
            // For testing if you don't have a db setup
            //return Json(new List<User> {new User { Id = 1, Name = "Bridge", Email = "lol@no.co", Password = "******" },});
            return Json(Models.User.GetAll(25, 0));
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Json(Models.User.Get(id));
        }

        [HttpPost("register")]
        public ActionResult Register()
        {
            User user = new User {
                Name = Request.Form["name"],
                Email = Request.Form["email"],
                Password = Request.Form["password"]
            };
            user.hashPassword();
            if (user.Add())
                return Json(user);
            return (Json(new {Status = "Error", Message = "Something went wrong."}));
        }

        [HttpPost("login")]
        public ActionResult Login()
        {
            User user = Models.User.GetByName(Request.Form["name"]);

            if (user == null)
                return StatusCode(404, "Invalid username/password");
            if (user.validatePassword(Request.Form["password"]))
            {
                // Create user session
                return Json(user);
            }
            return StatusCode(401, "Invalid username/password");
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public ActionResult<string> Update(int id, [FromBody] string value)
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
