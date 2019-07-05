using System;  
using System.Collections.Generic;  
using System.Threading.Tasks;
using PokeClinic.Repository;

namespace PokeClinic.Models
{
    public class Inventory {

        private InventoryRepository InventoryRepository;
        public Inventory() {
            InventoryRepository = new InventoryRepository();
        }
        
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public Int64 ItemQuantity { get; set; }
        public string RestorationAmount { get; set; }

        public string TypeLimitation {get; set; }

        public IEnumerable<Inventory> GetAll() {
            return  InventoryRepository.GetAll();
        }

        public bool AddOrUpdate(Inventory item) {
            return  InventoryRepository.AddOrUpdate(item);
        }

        public Inventory Find(string Name) {
            return  InventoryRepository.Find(Name);
        }

        public bool Delete(string name)
        {
            return  InventoryRepository.Delete(name);
        }
    }   
}