using System;

namespace PokeClinic.Models.Requests
{
    public class OrderUpdate
    {
        public Int64 id { get; set; }
        public DateTime OrderDate { get; set; }

    }
}