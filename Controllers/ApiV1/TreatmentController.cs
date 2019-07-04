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
        
        public static string nextAptSQL = "SELECT day, COUNT(id) AS numBookings FROM schedule GROUP BY schedule.day HAVING numBookings < 3 ORDER BY schedule.day ASC;";

        [HttpGet("{type}")]
        public ActionResult getAvailability(string type){
            TreatmentReturn treatment = TreatmentHandler.getAvailibleTreatment(type);
            return Json(treatment);
        }

        [HttpGet("next/available")]
        public ActionResult nextAppt(){
            return Json(TreatmentHandler.scheduleSqlRunner(nextAptSQL));
        }
    }
}