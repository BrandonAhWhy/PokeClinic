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
        
        // GET api/inventory
        [HttpGet]
        public ActionResult GetAll() {
            return Json(Inventory.GetAll());
        }

        [HttpGet("{name}")]
        public ActionResult Get(string name)
        {
            return Json(Inventory.Get(name));
        }

        

        // POST: api/inventory
        [HttpPost]
        public ActionResult Add(Inventory inventory) {
            var response =  new Inventory();
            if (response.AddOrUpdate(inventory)){
                return Json("Added: "+ inventory.Name);
            }

            return RedirectToAction("/");
        }


    }
}