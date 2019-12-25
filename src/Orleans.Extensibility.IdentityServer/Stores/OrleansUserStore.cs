using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Stores
{
    public sealed class OrleansUserStore<TUser> : IOrleansUserStore<TUser> where TUser: UserState
    {
        private readonly IClusterClient _clusterClient;

        public OrleansUserStore(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }
        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var userProfileGrain = await  FindByUsername(username);
            var userData = await userProfileGrain.GetUserData();
            if (userData.SubjectId != null)
            {
                return userData?.Password.Equals(password.Sha512(),StringComparison.InvariantCulture) ?? false;
            }

            return false;
        }

        public async Task<bool> Create(TUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task<IUserProfileGrain> FindByUsername(string username)
        {
            if(string.IsNullOrWhiteSpace(username)) throw  new ArgumentNullException(nameof(username),"Username cannot be null");
            return _clusterClient.GetGrain<IUserProfileGrain>(username.ToUpperInvariant());
        }

        public async Task<bool> Create(UserState user)
        {
            try
            {
                if (user is null) throw new ArgumentNullException(nameof(user), "User Data cannot be null");
                if (string.IsNullOrWhiteSpace(user.Username)) throw new ArgumentNullException(nameof(user.Username), "Username cannot be null");

                var grain = _clusterClient.GetGrain<IUserProfileGrain>(user.Username.ToUpperInvariant());
                var exists = await grain.GetProfileData();
                if (exists?.Username != null)
                {
                    return false;
                }

                await grain.Create(user.Email, user.Username, user.Password);
                foreach (var userClaim in user.Claims)
                {
                    await grain.SetClaim(userClaim.Key, userClaim.Value);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        public void Dispose()
        {
            //DO Not Dispose Client as it's Injected
        }

        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetNormalizedUserNameAsync(TUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task SetUserNameAsync(TUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}