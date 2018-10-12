
using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Orleans.Extensibility.IdentityServer.Mappers;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    internal class ClientGrain : Grain<ClientState>, IClientGrain
    {
        public Task<IdentityServer4.Models.Client> GetClientData()
        {
            var clientData = State.OrleansClient?.ToModel();
            if (clientData != null)
                clientData.ClientId = this.GetPrimaryKeyString();
            return Task.FromResult(State.OrleansClient?.ToModel());
        }

        public Task Create(IdentityServer4.Models.Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            State.OrleansClient = client.ToEntity();
            return WriteStateAsync();
        }
    }
}