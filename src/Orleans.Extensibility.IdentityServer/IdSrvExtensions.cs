using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Orleans.CodeGeneration;
using Orleans.Extensibility.IdentityServer.Grains;
using Orleans.Extensibility.IdentityServer.Services;
using Orleans.Extensibility.IdentityServer.Stores;
using Orleans.Indexing;

[assembly: KnownAssembly(typeof(Claim))]
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    //TODO use IdentityUser and Base Class for Conpatibility
    public static class IdSrvExtensions
    {
        public static IIdentityServerBuilder AddOrleansStores(this IIdentityServerBuilder builder)
        {
            builder
                .AddOrleansPersistenGrantStore()
                .AddOrleansClientStore()
                .AddOrleansUserStore()
                .AddOrleansProfileStore()
                .AddOrleansResourceOwnerPasswordValidator();

            return builder;
        }

        public static IIdentityServerBuilder AddOrleansResourceOwnerPasswordValidator<TUser>(this IIdentityServerBuilder builder) where TUser: UserState
        {
            builder.AddResourceOwnerValidator<OrleansResourceOwnerPasswordValidator<TUser>>();
            return builder;
        } 
        public static IIdentityServerBuilder AddOrleansResourceOwnerPasswordValidator(this IIdentityServerBuilder builder)
        {
            builder.AddResourceOwnerValidator<OrleansResourceOwnerPasswordValidator<UserState>>();
            return builder;
        }

        public static IIdentityServerBuilder AddOrleansUserStore(this IIdentityServerBuilder builder)
        {
            builder.AddOrleansUserStore<UserState>();
            return builder;
        }
        public static IIdentityServerBuilder AddOrleansUserStore<TUser>(this IIdentityServerBuilder builder) where TUser: UserState
        {
            builder.Services.AddSingleton<IOrleansUserStore<TUser>, OrleansUserStore<TUser>>();
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
            builder?.Services.TryAddSingleton<IPersistedGrantStore, OrleansPersistedGrantStore>();

            return builder;
            
        }
    }
}