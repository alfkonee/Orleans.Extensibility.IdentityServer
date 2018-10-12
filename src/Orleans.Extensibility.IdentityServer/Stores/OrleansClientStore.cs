
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Stores
{
    public class OrleansClientStore : IClientStore
    {
        private readonly IClusterClient _clusterClient;

        public OrleansClientStore(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
        }

        public async Task<IdentityServer4.Models.Client> FindClientByIdAsync(string clientId)
        {
            var clientData = await _clusterClient.GetGrain<IClientGrain>(clientId).GetClientData();
            return clientData;
        }
    }

    public class OrleansApiResourceStore : IResourceStore
    {
        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            throw new NotImplementedException();
        }
    }
}