using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Orleans.CodeGeneration;
using Orleans.Extensibility.IdentityServer.Services;
using Orleans.Extensibility.IdentityServer.Stores;
using Orleans.Indexing;

[assembly: KnownAssembly(typeof(Claim))]
namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdSrvExtensions
    {
        public static IIdentityServerBuilder AddOrleansUserStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IUserStore, OrleansUserStore>();
            builder.AddProfileService<OrleansProfileService>();
            builder.AddResourceOwnerValidator<OrleansResourceOwnerPasswordValidator>();

            return builder;
        }
        public static IIdentityServerBuilder AddOrleansProfileStore(this IIdentityServerBuilder builder)
        {
            builder.AddProfileService<OrleansProfileService>();

            return builder;
        }
        public static IIdentityServerBuilder AddOrleansClientStore(this IIdentityServerBuilder builder)
        {
            builder.AddClientStore<OrleansClientStore>();

            return builder;
        }

        public static IIdentityServerBuilder AddOrleansPersistenGrantStore(this IIdentityServerBuilder builder)
        {
            builder.Services.TryAddSingleton<IPersistedGrantStore, OrleansPersistedGrantStore>();

            return builder;
        }
    }
}