using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PokeClinic.Models;

namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/inventory")]
    [ApiController]
    public class InventoryController: Controller
    {
        private  Inventory Inventory;
        // GET api/inventory
        [HttpGet]
        public ActionResult<IEnumerable<Inventory>> GetAll() 
        {
            Inventory = new Inventory();
            return Ok( Inventory.GetAll());
        }

        [HttpGet("{name}")]
        public ActionResult<Inventory> Find(string name)
        {
            Inventory = new Inventory();
            return Ok( Inventory.Find(name));
        }

        // PUT: api/inventory
        [HttpPut]
        public ActionResult<bool> Add([FromBody]Inventory inventory) 
        {
            Inventory = new Inventory();
            if ( Inventory.AddOrUpdate(inventory)) {
                return Json("Added: "+ inventory.Name);
            }

            return RedirectToAction("/");
        }


        //DELETE: api/inventory
        [HttpDelete("{name}")]
        public ActionResult<bool> Delete(string name){
            Inventory = new Inventory();
            if ( Inventory.Delete(name)){
                return Json("Removed item: "+ name);
            }

            return RedirectToAction("/");
        }
    }
}