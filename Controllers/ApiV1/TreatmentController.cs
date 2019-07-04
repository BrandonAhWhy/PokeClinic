using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PokeClinic.Models;

namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/treatment")]
    [ApiController]
    public class TreatmentController: Controller{

        [HttpGet("{type}")]
        public ActionResult getAvailability(string type){
            return Json(TreatmentHandler.getAvailability(type));
        }
    }
}