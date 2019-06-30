using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PokeClinic.Models;

namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/inventory")]
    [ApiController]
    public class InventoyrController: Controller
    {
        // GET api/inventory
        [HttpGet]
        public ActionResult Get(int id)
        {
            return Json(Inventory.Get(id));
        }

        // POST: api/inventory

        [HttpPost]
        public ActionResult Add(Inventory inventory) {
            var response =  new Inventory();
            if (response.Add(inventory)){
                return Json("Added: "+ inventory.Name);
            }

            return RedirectToAction("/");
        }


    }
}