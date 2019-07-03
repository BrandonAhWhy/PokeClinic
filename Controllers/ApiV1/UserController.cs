using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PokeClinic.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Specialized;
using Microsoft.Extensions.Primitives;



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
        [BearerTokenFilter]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            // Console.Write(test);
            var test2=(HttpContext);
            return Json(Models.User.Get(id));
        }

        [HttpPost("register")]
        public ActionResult Register()
        {
            Console.Write(ModelState);
            User user = new User {
                Name = Request.Form["name"],
                Email = Request.Form["email"],
                Password = Request.Form["password"]
            };
            if (Models.User.GetByName(user.Name) == null){
                user.hashPassword();
                if (user.Add())
                    return Json(user);
                return (Json(new {Status = "Error", Message = "Something went wrong."}));
            }else{
                return BadRequest("Username already exists");
            }
        }

        [HttpPost("login")]
        public ActionResult Login()
        {
            User user = Models.User.GetByName(Request.Form["name"]);
            if (user == null)
                return StatusCode(404, "Invalid username/password");
            if (user.validatePassword(Request.Form["password"]))
            {
                //gen user token (null if failed)
                user.Token = Models.TokenController.GenToken(user);
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
