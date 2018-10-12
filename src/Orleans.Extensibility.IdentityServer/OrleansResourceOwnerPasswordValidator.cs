using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using Orleans.Extensibility.IdentityServer.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    public class OrleansResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserStore _userRepository;
 
        public OrleansResourceOwnerPasswordValidator(IUserStore userRepository)
        {
            _userRepository = userRepository;
        }
 
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (await _userRepository.ValidateCredentials(context.UserName, context.Password))
            {
                var user = await (await _userRepository.FindByUsername(context.UserName)).GetUserData();
                context.Result = new GrantValidationResult(user.SubjectId, OidcConstants.AuthenticationMethods.Password);
            }
 
        }
    }
}