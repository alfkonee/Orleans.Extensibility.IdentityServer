
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    public interface IClientGrain : IGrainWithStringKey
    {
        Task Create(Client client);
        Task<Client> GetClientData();
    }
}