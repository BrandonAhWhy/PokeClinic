using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PokeClinic.Models;

namespace PokeClinic.Controllers.ApiV1
{
    [Route("api/v1/schedule")]
    [ApiController]
    public class ScheduleController : Controller 
    {
        private Schedule Schedule;
        // GET api/schedule
        [HttpGet]
        public ActionResult<IEnumerable<Schedule>> GetAll()
        {
            Schedule = new Schedule();
            return Ok( Schedule.GetAll());
        }

        [HttpGet("{day}")]
        public ActionResult<Schedule> Find(Int64 day)
        {
            Schedule = new Schedule();
            return Ok( Schedule.Find(day));
        }

        // PUT: api/schedule
        [HttpPut]
        public ActionResult<bool> Add([FromBody]Schedule schedule)
        {
            Schedule = new Schedule();
            if ( Schedule.AddOrUpdate(schedule)) {
                return Json("Added: " + schedule.day);
            }

            return RedirectToAction("/");
        }

        //DELETE: api/schedule
        [HttpDelete("{day}")]
        public ActionResult<bool> Delete(Int64 day)
        {
            Schedule = new Schedule();
            if( Schedule.Delete(day)) {
                return Json("Removed item: " + day);
            }

            return RedirectToAction("/");
        }
    }
}