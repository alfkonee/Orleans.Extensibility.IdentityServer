using System.Threading.Tasks;
using IdentityServer4.Models;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    //Name == Key
    public interface IIdentityResourceGrain : IGrainWithStringKey
    {
        Task Create(IdentityResource resouce);
        Task<IdentityResource> GetData();
    }
}