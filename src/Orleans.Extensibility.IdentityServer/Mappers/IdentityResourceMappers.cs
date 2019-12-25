using AutoMapper;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Mappers
{
    internal static class IdentityResourceMappers
    {
        static IdentityResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityResourceMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        internal static IdentityServer4.Models.IdentityResource ToModel(this OrleansIdentityResource orleansClient)
        {
            return Mapper.Map<IdentityServer4.Models.IdentityResource>(orleansClient);
        }

        internal static OrleansIdentityResource ToEntity(this IdentityServer4.Models.IdentityResource client)
        {
            return Mapper.Map<OrleansIdentityResource>(client);
        }
    }
}