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

        public async Task<IEnumerable<Inventory>> GetAll() {
            return await InventoryRepository.GetAll();
        }

        public async Task<bool> AddOrUpdate(Inventory item) {
            return await InventoryRepository.AddOrUpdate(item);
        }

        public async Task<Inventory> Find(string Name) {
            return await InventoryRepository.Find(Name);
        }

        public async Task<bool> Delete(string name)
        {
            return await InventoryRepository.Delete(name);
        }
    }   
}