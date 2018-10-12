using System.Threading.Tasks;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Stores
{
    public interface IUserStore
    {
        Task<bool> Create(UserState user);
        Task<IUserProfileGrain> FindByUsername(string username);
        Task<bool> ValidateCredentials(string username, string password);
    }
}