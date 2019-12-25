using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using Orleans.Extensibility.IdentityServer.Grains;
using Orleans.Extensibility.IdentityServer.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    public class OrleansResourceOwnerPasswordValidator<TUser> : IResourceOwnerPasswordValidator where TUser: UserState
    {
        private readonly IOrleansUserStore<TUser> _userRepository;
 
        public OrleansResourceOwnerPasswordValidator(IOrleansUserStore<TUser> userRepository)
        {
            _userRepository = userRepository;
        }
 
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (await _userRepository.ValidateCredentials(context.UserName, context.Password))
            {
                var user = await (await _userRepository.FindByUsername(context.UserName)).GetUserData();
                
                context.Result = new GrantValidationResult(user.SubjectId, OidcConstants.AuthenticationMethods.Password,
                    user.Claims.Select(c=> new Claim(c.Key,c.Value)));
            }
 
        }
    }
}