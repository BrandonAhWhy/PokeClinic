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
        public async Task<bool> AddOrUpdate(Schedule _schedule) {
            string sql = "INSERT INTO schedule (day) VALUES (@Day)";

            bool success = false;
            bool foundSlot = false;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                while(!foundSlot) {
                    int dayCount = await Find(_schedule.Day);
                    if(dayCount >= 3) {
                        return;
                    } else {
                        var affectedRows = await conn.ExecuteAsync(sql, _schedule);
                        success = (affectedRows > 0) ? true : false;
                        foundSlot = true;
                    }
                }
            }
            return success;
        }

        public async Task<Schedule> Find(Int64 Day) {
            string sql = "COUNT day FROM schedule WHERE day = @Day";
            Schedule Schedule = null;

            using (MySqlConnect conn = PokeDB.NewConnection()) {
                Schedule = await conn.QueryFirstAsync<Schedule>(sql, new { Id = Id});
            }
            return Schedule;
        }

        public async Task<Schedule> Find(string name) {
            string sql = "SELECT * FROM schedule WHERE name = @Name";
            Schedule Schedule = null;

            using (MySqlConnection conn = PokeDB.NewConnection()) {
                Schedule = await conn.QueryFirstAsync<Schedule>(sql, new { name = name});
            }
            return Schedule;
        }

        public async Task<IEnumerable<Schedule>> GetAll() {
            string sql = "SELECT * FROM schedule";

            using (MySqlConnection conn = PokeDB.NewConnection()) {
                var ScheduleCollection = await conn.QueryAsync<Schedule>(sql);
                return ScheduleCollection;
            }
        }

        public async Task<bool> Delete(string name) {
            string sqlDelete = "DELETE FROM schedule WHERE Id = @Id";
            bool success = false;
            Schedule schedule = new Schedule();
            using (MySqlConnection conn = PokeDB.NewConnection()) {
                try {
                    schedule = await Find(name);
                    var affectedRows = await conn.ExecuteAsync(sqlDelete, schedule);
                    success = (affectedRows > 0) ? true : false;
                } catch {
                    return success;
                }
            }
            return success;
        }
    }
}