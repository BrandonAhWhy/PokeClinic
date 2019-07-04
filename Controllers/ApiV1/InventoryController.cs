using System;
using System.Collections.Generic;
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
<<<<<<< HEAD
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAll() 
        {
            Inventory = new Inventory();
            return Ok(await Inventory.GetAll());
=======
        public ActionResult GetAll() {
            return Json(Inventory.GetAll());
>>>>>>> testing/Mikhail_Inventory-Read
        }

        [HttpGet("{name}")]
        public ActionResult Get(string name)
        {
<<<<<<< HEAD
            Inventory = new Inventory();
            return Ok(await Inventory.Find(name));
        }

        // PUT: api/inventory
        [HttpPut]
        public async Task<ActionResult<bool>> Add([FromBody]Inventory inventory) 
        {
            Inventory = new Inventory();
            if (await Inventory.AddOrUpdate(inventory)) {
=======
            return Json(Inventory.Get(name));
        }        

        // POST: api/inventory
        [HttpPost]
        public ActionResult Add(Inventory inventory) {
            var response =  new Inventory();
            if (response.AddOrUpdate(inventory)){
>>>>>>> testing/Mikhail_Inventory-Read
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