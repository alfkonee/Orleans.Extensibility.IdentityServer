using AutoMapper;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Mappers
{
    internal static class ClientMappers
    {
        static ClientMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        internal static IdentityServer4.Models.Client ToModel(this OrleansClient orleansClient)
        {
            return Mapper.Map<IdentityServer4.Models.Client>(orleansClient);
        }

        internal static OrleansClient ToEntity(this IdentityServer4.Models.Client client)
        {
            return Mapper.Map<OrleansClient>(client);
        }
    }
}