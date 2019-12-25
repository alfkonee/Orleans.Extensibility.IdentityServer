using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Orleans.Providers;

namespace Orleans.Extensibility.IdentityServer.Grains
{
    [StorageProvider]
    public class UserProfileGrain : Grain<UserState>, IUserProfileGrain
    {
        private IProfileGrain ProfileGrain => GrainFactory.GetGrain<IProfileGrain>(this.GetPrimaryKeyString());
        public async Task Create(string email, string username, string password)
        {
            var state = await ProfileGrain.GetProfileData();

            if (state?.Username != null) throw new InvalidOperationException("Profile already exist.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));

            State.Email = email;
            State.Username = username;
            State.Password = password.Sha512();
            State.SubjectId = this.GetPrimaryKeyString();
            await ProfileGrain.UpdateFullProfile(new UserProfile { SubjectId = this.GetPrimaryKeyString(), Email = email, Username = username});
            await WriteStateAsync();
        }

        public Task<UserProfile> GetProfileData() => ProfileGrain.GetProfileData();

        public  Task SetClaim(string claim, string value)
        {
            if (string.IsNullOrWhiteSpace(claim)) throw new ArgumentNullException(nameof(claim));
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
            if (State == null) throw new InvalidOperationException("Profile doesn't exist.");

            State.Claims[claim] = value;
            ProfileGrain.UpdateClaim(claim, value);
            return WriteStateAsync();
        }

        public async Task RemoveClaim(string claim)
        {
            if (string.IsNullOrWhiteSpace(claim)) throw new ArgumentNullException(nameof(claim));
            if (State == null) throw new InvalidOperationException("Profile doesn't exist.");

            if (State.Claims.Remove(claim))
            {
                await WriteStateAsync();
            }
        }

        public  Task<UserState> GetUserData()
        {
           // if (State == null) throw new InvalidOperationException("Profile doesn't exist.");
            return Task.FromResult(State);
        }
    }
}