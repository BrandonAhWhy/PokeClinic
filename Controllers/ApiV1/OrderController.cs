using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;
using Dapper.Contrib.Extensions;
using PokeClinic.Models.Orders;
using System.Transactions;


namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : Controller
    {
        // GET api/user
     
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return Ok(Order.GetAll(25,0));
        }

        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            return Ok(Order.Get(id));
        }
        
        [HttpPost]
        public ActionResult<Order> Add([FromBody]IEnumerable<Models.Requests.OrderAdd> orderAdd)
        {
            Order order = new Order();
            //datetime set on insert
            if (order.Add(orderAdd)){
                return Ok(order);

            }
            return BadRequest("Failed to create an order");
        }

        [HttpPut("{id}/update")]
        public ActionResult<string> Update(Int64 Id, [FromBody]IEnumerable<Models.Orders.ItemOrder> orderUpdate)
        {
            Order order = Order.Get(Id);
            if (order == null) {
                return StatusCode(404, "Invalid order");
            }
            if (order.Update(orderUpdate))
                return Json(order);
            else
                return BadRequest("Failed to update");
        }

        [HttpPut("{id}/receive")]
        public ActionResult<string> Receive(int id)
        {
            Order order = Order.Get(id);
            if (order == null) {
                return StatusCode(404, "Invalid order");
            }

            if (order.Receive())
                return StatusCode(200, "Order Successfully received");
            else
                return BadRequest("Failed to update");
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            Order order = Order.Get(id);

            if (order == null) {
                return StatusCode(404, "Invalid user");
            }

            order.Delete();
            return  StatusCode(200, "Order deleted successfully");
        }
    }
}
