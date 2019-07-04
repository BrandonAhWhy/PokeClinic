using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspnetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAll()
        {
            Schedule = new Schedule();
            return Ok(await Schedule.GetAll());
        }

        [HttpGet("{day}")]
        public async Task<ActionResult<Schedule>> Find(Int64 day)
        {
            Schedule = new Schedule();
            return Ok(await Schedule.Find(day));
        }

        // PUT: api/schedule
        [HttpPut]
        public async Task<ActionResult<bool>> Add([FromBody]Schedule schedule)
        {
            Schedule = new Schedule();
            if (await Schedule.AddOrUpdate(schedule)) {
                return Json("Added: " + schedule.Day);
            }

            return RedirectToAction("/");
        }

        //DELETE: api/schedule
        [HttpDelete("{day}")]
        public async Task<ActionResult<bool>> Delete(Int64 day)
        {
            Schedule = new Schedule();
            if(await Schedule.Delete(day)) {
                return Json("Removed item: " + day);
            }

            return RedirectToAction("/");
        }
    }
}