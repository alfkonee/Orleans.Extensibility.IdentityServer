using System;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Stores
{
    public class OrleansUserStore : IUserStore
    {
        private readonly IClusterClient _clusterClient;

        public OrleansUserStore(IClusterClient clusterClient)
        {
            _clusterClient = clusterClient;
        }
        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var user = await  FindByUsername(username);
            if (user != null)
            {
                return (await user.GetUserData())?.Password.Equals(password.Sha512())?? false;
            }

            return false;
        }

        /// <summary>
        /// Finds the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task<IUserProfileGrain> FindByUsername(string username)
        {
            return _clusterClient.GetGrain<IUserProfileGrain>(username.ToLower());
        }

        public async Task<bool> Create(UserState user)
        {
            try
            {
                var grain = _clusterClient.GetGrain<IUserProfileGrain>(user.Username.ToLower());
                var exists = await grain.GetProfileData();
                if (exists.Username != null)
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
                throw ex;
            }
           
        }

    }
}