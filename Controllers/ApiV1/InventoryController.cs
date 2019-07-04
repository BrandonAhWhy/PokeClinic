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
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAll() 
        {
            Inventory = new Inventory();
            return Ok(await Inventory.GetAll());
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Inventory>> Get(string name)
        {
            Inventory = new Inventory();
            return Ok(await Inventory.Find(name));
        }

        // PUT: api/inventory
        [HttpPut]
        public async Task<ActionResult<bool>> Add([FromBody]Inventory inventory) 
        {
            Inventory = new Inventory();
            if (await Inventory.AddOrUpdate(inventory)) {
                return Json("Added: "+ inventory.Name);
            }
            return RedirectToAction("/");
        }


    }
}