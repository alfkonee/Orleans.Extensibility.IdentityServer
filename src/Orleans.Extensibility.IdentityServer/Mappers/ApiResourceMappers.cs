using AutoMapper;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Mappers
{
    internal static class ApiResourceMappers
    {
        static ApiResourceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        internal static IdentityServer4.Models.ApiResource ToModel(this OrleansApiResource orleansApiResource)
        {
            return Mapper.Map<IdentityServer4.Models.ApiResource>(orleansApiResource);
        }

        internal static OrleansApiResource ToEntity(this IdentityServer4.Models.ApiResource client)
        {
            return Mapper.Map<OrleansApiResource>(client);
        }
    }
}