using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PokeClinic.Models;
using PokeClinic.Repository;

namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/inventory")]
    [ApiController]
    public class InventoryController: Controller
    {
        
        // GET api/inventory
        [HttpGet]
        public ActionResult GetAll() {
            return Json(InventoryRepository.GetAll());
        }

        [HttpGet("{name}")]
        public ActionResult Get(string name)
        {
            return Json(InventoryRepository.Find(name));
        }

        

        // POST: api/inventory
        [HttpPut]
        public ActionResult Add(Inventory inventory) {
            if (InventoryRepository.AddOrUpdate(inventory)){
                return Json("Added: "+ inventory.Name);
            }

            return RedirectToAction("/");
        }


    }
}