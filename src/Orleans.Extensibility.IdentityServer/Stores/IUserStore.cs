using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Stores
{
    public interface IOrleansUserStore<T>: IUserStore<T> where T: UserState
    {
        Task<bool> Create(T user);
        Task<IUserProfileGrain> FindByUsername(string username);
        Task<bool> ValidateCredentials(string username, string password);
    }
}