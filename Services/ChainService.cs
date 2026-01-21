using Microsoft.JSInterop;
using StoreManager.Models;
using StoreManager.Services;
using System;


namespace StoreManager.Services
{
    public class ChainService
    {

        private readonly IJSRuntime _jsRuntime;
        private readonly StoreService _storeService;
        private List<Chain> _chains = new List<Chain>();
        private const string LocalStorageKey = "chains";
        public ChainService(IJSRuntime jsRuntime, StoreService storeService)
        {
            _jsRuntime = jsRuntime;
            _storeService = storeService;
        }   
        public Task<List<Chain>> GetAllChainsAsync()
        {
            return Task.FromResult(_chains);
        }
        public async Task AddChainAsync(Chain chain)
        {
            if (_chains.Any(c => c.Name == chain.Name))
                throw new InvalidOperationException("Kæde med samme navn findes allerede");
            
            chain.CreatedOn = DateTime.UtcNow;
            chain.ModifiedOn = DateTime.UtcNow;

            _chains.Add(chain);
            await SaveToLocalStorageAsync();
        }
        public async Task UpdateChainAsync(Chain chain)
        {
            var existingChain = _chains.FirstOrDefault(c => c.Id == chain.Id);
            if (existingChain == null)
                throw new InvalidOperationException("Kæde findes ikke");

            if (_chains.Any(c => c.Name == chain.Name))
                throw new InvalidOperationException("Kæde med samme navn findes allerede");

            existingChain.Name = chain.Name;
            existingChain.ModifiedOn = DateTime.UtcNow;
            await SaveToLocalStorageAsync();
        }

        private async Task SaveToLocalStorageAsync()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(_chains);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", LocalStorageKey, json);
        }

        public async Task LoadFromLocalStorageAsync()
        {
            if (_isLoaded)
                return;

            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", LocalStorageKey);

            if (!string.IsNullOrEmpty(json))
            {
                _chains = System.Text.Json.JsonSerializer.Deserialize<List<Chain>>(json)! ?? new List<Chain>();
            }

            _isLoaded = true;
        }

        public async Task<List<Chain>> GetAllchainsAsync()
        {
            await LoadFromLocalStorageAsync();
            return _chains;
        }

        public async Task DeleteChainAsync(Guid chainId)
        {
            var chain = _chains.FirstOrDefault(c => c.Id == chainId);
            if (chain == null)
                throw new InvalidOperationException("Kæde findes ikke");

            var stores = await _storeService.GetAllStoresAsync();
            if (stores.Any(s => s.ChainId == chainId))
                throw new InvalidOperationException("Kan ikke slette kæde med tilknyttede butikker");

             _chains.Remove(chain);
             await SaveToLocalStorageAsync();
        }

        private bool _isLoaded = false;


        private async Task SaveTolocalStorageAsync()
        {
            if (!_isLoaded)
                return;

            var json = System.Text.Json.JsonSerializer.Serialize(_chains);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", LocalStorageKey, json);
        }
    }
}

