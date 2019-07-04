using System;
using System.Collections.Generic;

namespace PokeClinic.Models.Requests
{

    public class OrderAdd
    {
        public Int64 OrderId { get; set; }
        public Int64 ItemId { get; set; }
        public int Quantity { get; set; }
    } 
}