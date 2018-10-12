using System.Threading.Tasks;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    public interface IUserProfileGrain : IGrainWithStringKey
    {
        Task Create(string email, string username, string password);

        Task<UserProfile> GetProfileData();

        Task SetClaim(string claim, string value);

        Task RemoveClaim(string claim);
        Task<UserState> GetUserData();
    }
}