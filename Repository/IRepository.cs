using System;
using System.Collections.Generic;

namespace PokeClinic.Models
{
     public interface IRepository<T>
     {
          Int64 Id { get; set; }
          void Add(T newEntity);
          void Remove(T entity);
     }
}