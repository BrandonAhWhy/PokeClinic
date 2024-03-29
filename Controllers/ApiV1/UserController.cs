﻿using System.Collections.Generic;
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
        public ActionResult<IEnumerable<Models.User>> GetAll()
        {
            // For testing if you don't have a db setup
            //return Json(new List<User> {new User { Id = 1, Name = "Bridge", Email = "lol@no.co", Password = "******" },});
            return Ok(Models.User.GetAll(25, 0));
        }

        // GET api/user/5
        [BearerTokenFilter(authAdmin=true, authID=true)]
        [HttpGet("{id}")]
        public ActionResult<Models.User> Get(int id)
        {
            return Ok(Models.User.Get(id));
        }

        [HttpPost("register")]
        public ActionResult<Models.User> Register([FromBody]Models.Requests.UserRegister userRegister)
        {
            User user = new User {
                CustomerName = userRegister.Name,
                CustomerEmail = userRegister.Email,
                CustomerPassword = userRegister.Password,
            };
            if (Models.User.GetByName(user.CustomerName) == null){
                user.hashPassword();
                if (user.Add())
                    return Ok(user);
                return (Json(new {Status = "Error", Message = "Something went wrong."}));
            }else{
                return BadRequest("Username already exists");
            }
        }

        [HttpPost("login")]
        public ActionResult<Models.User> Login([FromBody]Models.Requests.UserLogin userLogin)
        {
            User user = Models.User.GetByName(userLogin.Name);
            if (user == null)
                return StatusCode(404, "Invalid username/password");
            if (user.validatePassword(userLogin.Password))
            {
                //gen user token (null if failed)
                user.Token = Models.TokenController.GenToken(user);
                // Create user session
                return Ok(user);
            }
            return StatusCode(401, "Invalid username/password");
        }

        

        // PUT api/user/5
        [BearerTokenFilter]
        [HttpPut("{id}")]
        public ActionResult<string> Update(int id, [FromBody]Models.Requests.UserUpdate userUpdate)
        {
            User user = Models.User.Get(id);
            if (user == null) {
                return StatusCode(404, "Invalid user");
            }

            user.CustomerName = (userUpdate.Name != null) ? userUpdate.Name : user.CustomerName;
            user.CustomerEmail = (userUpdate.Email != null) ? userUpdate.Email : user.CustomerEmail;
            if (user.Update())
                return Json(user);
            else
                return BadRequest("Failed to update");
        }

        // DELETE api/user/5
        [BearerTokenFilter]
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            User user = Models.User.Get(id);

            if (user == null) {
                return StatusCode(404, "Invalid user");
            }

            user.Delete();
            return  StatusCode(204, "User deleted successfully");
        }
    }
}
