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
        
        // GET api/inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAll() {
            return Ok(await Inventory.GetAll());
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Inventory>> Get(string name)
        { 
            return Ok(await Inventory.Find(name));
        }

        // POST: api/inventory
        [HttpPut]
        public async Task<ActionResult<bool>> Add(Inventory inventory) {
            if (await Inventory.AddOrUpdate(inventory)) {
                return Json("Added: "+ inventory.Name);
            }
            return RedirectToAction("/");
        }


        //DELETE: api/inventory
        [HttpDelete("{id}")]
        public ActionResult Delete(Int64 id){
            var response = new Inventory();
            if (response.Delete(id)){
                return Json("Removed item: "+ id);
            }

            return RedirectToAction("/");
        }


    }
}