using System;
using System.Collections.Generic;

namespace PokeClinic.Models
{
    public class Schedule {
        public Int64 Id { get; set; }
        public Int64 Day { get; set; }

        public async Task<IEnumerable<Schedule>> GetAll() {
            return await ScheduleRepository.GetAll();
        }

        public async Task<bool> Add(Schedule item) {
            return await ScheduleRepository.Add(item);
        }

        public async Task<bool> Delete(Int64 Id) {
            return await ScheduleRepository.Delete(Id);
        }
    }
}