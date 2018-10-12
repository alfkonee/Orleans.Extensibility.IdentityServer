using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Orleans.Indexing;
using Orleans.Providers;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    [StorageProvider]
    public class ProfileGrain : IndexableGrain<UserProfile,UserProfile>, IProfileGrain
    {
        public async Task<UserProfile> GetProfileData()
        {
            return State;
        }

        public async Task<bool> UpdateFullProfile(UserProfile data)
        {
            if (data != null)
                State = data;
            else
                return false;
            await WriteStateAsync();
            return true;
        }

        public async Task<bool> UpdateEmail()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePhoneNumber()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateClaim(string claim, string value)
        {
            if (string.IsNullOrWhiteSpace(claim)) throw new ArgumentNullException(nameof(claim));
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
            if (State == null) throw new InvalidOperationException("Profile doesn't exist.");
            State.Claims[claim] = value;
            
            await WriteStateAsync();
            return true;
        }

        public async Task<Claim> GetClaim(string claim)
        {
            throw new NotImplementedException();
        }
    }
}