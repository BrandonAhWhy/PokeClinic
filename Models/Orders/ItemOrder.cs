using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;
using Dapper.Contrib.Extensions;

namespace PokeClinic.Models.Orders
{
    public class ItemOrder
    {
        public Int64 ItemId {get; set;}
        public int Quantity {get; set;}
    }
}