using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Orleans.Concurrency;
using Orleans.Extensibility.IdentityServer.Mappers;
using Orleans.Indexing;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    public class ApiResourceGrain : Grain<ApiResourceState>, IApiResourceGrain
    {
        public ApiResourceGrain()
        {
            
        }
        public Task<ApiResource> GetData()
        {
            var clientData = State.ApiResource?.ToModel();
            if (clientData != null)
                clientData.Name = this.GetPrimaryKeyString();
            return Task.FromResult(State.ApiResource?.ToModel());
        }

        public Task Create(ApiResource resouce)
        {
            if (resouce == null) throw new ArgumentNullException(nameof(resouce));
            State.ApiResource = resouce.ToEntity();
            return WriteStateAsync();
        }

        public async Task<Immutable<HashSet<Guid>>> GetActiveWorkflowIdsSet()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveFromActiveWorkflowIds(HashSet<Guid> removedWorkflowId)
        {
            throw new NotImplementedException();
        }
    }

    public class ApiResourceIndexProps
    {

    }
}