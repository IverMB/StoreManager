using StoreManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreManager.Models;

namespace StoreManager.Services
{
    public class StoreService
    {
        private List<Store> _stores = new List<Store>();
        public Task<List<Store>> GetAllStoresAsync()
        {
            return Task.FromResult(_stores);
        }
        public Task AddStoreAsync(Store store)
        {
            if (_stores.Any(s => s.Storenumber == store.Storenumber))
            
                throw new InvalidOperationException("Butik med samme navne findes allerede");

            _stores.Add(store);
            return Task.CompletedTask;
        }
        public Task UpdateStoreAsync(Store store)
        {
            var existingStore = _stores.FirstOrDefault(s => s.Id == store.Id);
            if (existingStore == null) throw new InvalidOperationException("Butik findes ikke");

            existingStore.Storenumber = store.Storenumber;
            existingStore.Name = store.Name;
            existingStore.Address = store.Address;
            existingStore.PostalCode = store.PostalCode;
            existingStore.City = store.City;
            existingStore.PhoneNumber = store.PhoneNumber;
            existingStore.Email = store.Email;
            existingStore.StoreOwner = store.StoreOwner;
            existingStore.ModifiedOn = DateTime.UtcNow;
            existingStore.ChainId = store.ChainId;

            return Task.CompletedTask;
        }

        public Task DeleteStoreAsync(Guid storeId)
        {
            var store = _stores.FirstOrDefault(s => s.Id == storeId);
            if (store == null)
                throw new InvalidOperationException("Butik findes");

            _stores.Remove(store);
            return Task.CompletedTask;
        }



    }
}