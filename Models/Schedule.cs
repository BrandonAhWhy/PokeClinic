using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PokeClinic.Repository;

namespace PokeClinic.Models
{
    public class Schedule {
        private ScheduleRepository ScheduleRepository;
        public Schedule() {
            ScheduleRepository = new ScheduleRepository();
        }
        public int id {get; set;}
        public int day {get; set;}
        public int numBookings {get; set;}
        public async Task<IEnumerable<Schedule>> GetAll() {
            return await ScheduleRepository.GetAll();
        }

        public async Task<bool> AddOrUpdate(Schedule item) {
            return await ScheduleRepository.AddOrUpdate(item);
        }

        public async Task<Schedule> Find(string name) {
            return await ScheduleRepository.Find(name);
        }

        public async Task<Schedule> Find(Int64 day) {
            return await ScheduleRepository.Find(day);
        }

        public async Task<Int64> FindCount(Int64 day) {
            return await ScheduleRepository.FindCount(day);
        }

        public async Task<bool> Delete(string name) {
            return await ScheduleRepository.Delete(name);
        }

        public async Task<bool> Delete(Int64 day) {
            return await ScheduleRepository.Delete(day);
        }
    }
}