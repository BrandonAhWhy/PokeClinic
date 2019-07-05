using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Data;  
using System.Data.SqlClient;  
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;  
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PokeClinic.Models;
using Dapper;

namespace PokeClinic.Repository
{
    public class ScheduleRepository : IRepository<Schedule>
    {
        public bool AddOrUpdate(Schedule _schedule) {
            string sql = "INSERT INTO schedule (day) VALUES (@Day)";

            bool success = false;
            bool foundSlot = false;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                while(!foundSlot) {
                     Int64 dayCount =  FindCount(_schedule.day);
                    if(dayCount >= 3) {
                        break;
                    } else {
                        var affectedRows =  conn.Execute(sql, _schedule);
                        success = (affectedRows > 0) ? true : false;
                        foundSlot = true;
                    }
                }
            }
            return success;
        }

        public Schedule Find(Int64 Day) {
            string sql = "SELECT * FROM schedule WHERE day = @Day";
            Schedule Schedule = null;

            using (MySqlConnection conn = PokeDB.NewConnection()) {
                Schedule =  conn.QueryFirst<Schedule>(sql, new { Day = Day});
            }
            return Schedule;
        }

        public Schedule Find(string name) {
            string sql = "SELECT * FROM schedule WHERE name = @Name";
            Schedule Schedule = null;

            using (MySqlConnection conn = PokeDB.NewConnection()) {
                Schedule =  conn.QueryFirst<Schedule>(sql, new { name = name});
            }
            return Schedule;
        }

        public Int64 FindCount(Int64 Day) {
            string sql = "COUNT day FROM schedule WHERE day = @Day";
            Int64 count = 0;

            using(MySqlConnection conn = PokeDB.NewConnection()) {
                count =  conn.QueryFirst<Int64>(sql, new {Day = Day});
            }
            return count;
        }

        public IEnumerable<Schedule> GetAll() {
            string sql = "SELECT * FROM schedule";

            using (MySqlConnection conn = PokeDB.NewConnection()) {
                var ScheduleCollection =  conn.Query<Schedule>(sql);
                return ScheduleCollection;
            }
        }

        public bool Delete(string name) {
            string sqlDelete = "DELETE FROM schedule WHERE Id = @Id";
            bool success = false;
            Schedule schedule = new Schedule();
            using (MySqlConnection conn = PokeDB.NewConnection()) {
                try {
                    schedule =  Find(name);
                    var affectedRows =  conn.Execute(sqlDelete, schedule);
                    success = (affectedRows > 0) ? true : false;
                } catch {
                    return success;
                }
            }
            return success;
        }

        public bool Delete(Int64 day) {
            string sqlDelete = "DELETE FROM schedule WHERE Id = @Id";
            bool success = false;
            Schedule schedule = new Schedule();
            using (MySqlConnection conn = PokeDB.NewConnection()) {
                try {
                    schedule =  Find(day);
                    var affectedRows =  conn.Execute(sqlDelete, schedule);
                    success = (affectedRows > 0) ? true : false;
                } catch {
                    return success;
                }
            }
            return success;
        }
    }
}