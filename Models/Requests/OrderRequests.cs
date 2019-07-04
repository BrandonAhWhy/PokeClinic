using System;

namespace PokeClinic.Models.Requests
{
    public class OrderUpdate
    {
        public Int64 id { get; set; }
        public DateTime OrderDate { get; set; }

    }

    public class OrderAdd
    {
        public Int64 OrderId { get; set; }
        public Int64 ItemId { get; set; }
        public int Quantity { get; set; }
    } 
}