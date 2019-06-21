using System;
using System.Collections.Generic;

namespace PokeClinic.Models
{
     public interface IRepository
     {
          Int64 Id { get; set; }
          bool Add();
          bool Update();
          bool Delete();
     }
}