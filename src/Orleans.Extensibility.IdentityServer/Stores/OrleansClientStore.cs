
using System;
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

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var clientData = await _clusterClient.GetGrain<IClientGrain>(clientId).GetClientData();
            return clientData;
        }
    }
}