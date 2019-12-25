using System;
using System.Threading.Tasks;
using Orleans.Extensibility.IdentityServer.Mappers;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    public class IdentityResourceGrain : Grain<IdentityResourceState>, IIdentityResourceGrain
    {
        public Task<IdentityServer4.Models.IdentityResource> GetData()
        {
            var clientData = State.IdentityResource?.ToModel();
            if (clientData != null)
                clientData.Name = this.GetPrimaryKeyString();
            return Task.FromResult(State.IdentityResource?.ToModel());
        }

        public Task Create(IdentityServer4.Models.IdentityResource resouce)
        {
            if (resouce == null) throw new ArgumentNullException(nameof(resouce));
            State.IdentityResource = resouce.ToEntity();
            return WriteStateAsync();
        }
    }
}