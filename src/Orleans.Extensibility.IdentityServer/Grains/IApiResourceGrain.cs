using System.Threading.Tasks;
using IdentityServer4.Models;
using Orleans.Indexing;

namespace Orleans.Extensibility.IdentityServer.Grains
{

    //Name == Key
    public interface IApiResourceGrain : IGrainWithStringKey, IIndexableGrain<ApiResourceIndexProps>
    {
        Task Create(ApiResource resouce);
        Task<ApiResource> GetData();
    }
}