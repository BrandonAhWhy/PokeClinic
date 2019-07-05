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
        public IEnumerable<Schedule> GetAll() {
            return  ScheduleRepository.GetAll();
        }

        public bool AddOrUpdate(Schedule item) {
            return  ScheduleRepository.AddOrUpdate(item);
        }

        public Schedule Find(string name) {
            return  ScheduleRepository.Find(name);
        }

        public Schedule Find(Int64 day) {
            return  ScheduleRepository.Find(day);
        }

        public Int64 FindCount(Int64 day) {
            return  ScheduleRepository.FindCount(day);
        }

        public bool Delete(string name) {
            return  ScheduleRepository.Delete(name);
        }

        public bool Delete(Int64 day) {
            return  ScheduleRepository.Delete(day);
        }
    }
}