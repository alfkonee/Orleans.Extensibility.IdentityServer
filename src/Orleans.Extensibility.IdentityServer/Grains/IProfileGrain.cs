using System.Security.Claims;
using System.Threading.Tasks;
using Orleans.Indexing;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    public interface IProfileGrain : IGrainWithStringKey, IIndexableGrain<UserProfile>
    {
        Task<UserProfile> GetProfileData();
        Task<bool> UpdateFullProfile(UserProfile data);
        Task<bool> UpdateEmail();
        Task<bool> UpdatePhoneNumber();
        Task<bool> UpdateClaim(string claim, string value);
        Task<Claim> GetClaim(string claim);
    }
}