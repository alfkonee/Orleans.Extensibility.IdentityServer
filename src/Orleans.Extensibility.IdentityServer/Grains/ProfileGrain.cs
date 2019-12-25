using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Orleans.Concurrency;
using Orleans.Indexing;
using Orleans.Providers;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    [StorageProvider]
    public class ProfileGrain : Grain<UserProfileState>, IProfileGrain
    {
        public async Task<UserProfile> GetProfileData()
        {
            return State.Profile;
        }

        public async Task<bool> UpdateFullProfile(UserProfile data)
        {
            if (data != null)
                State.Profile = data;
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
            State.Profile.Claims[claim] = value;
            
            await WriteStateAsync();
            return true;
        }

        public async Task<Claim> GetClaim(string claim)
        {
            throw new NotImplementedException();
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
}