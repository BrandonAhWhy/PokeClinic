using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; 
using PokeClinic.Models;



namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : Controller
    {
        // GET api/user
     
        [HttpGet]
        public ActionResult<string> GetAll()
        {
            return Ok("Todo");
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok("Todo");
        }
        
        [HttpPost("/place")]
        public ActionResult<string> PlaceOrder()
        {
            return Ok("Todo");
        }

        [HttpPost("/receive")]
        public ActionResult<string> ReceiveOrder()
        {
            return Ok("Todo");
        }

    }
}
