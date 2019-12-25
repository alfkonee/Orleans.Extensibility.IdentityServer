using AutoMapper;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Mappers
{
    /// <summary>
    /// AutoMapper configuration for OrleansIdentityResource
    /// Between model and entity
    /// </summary>
    internal class IdentityResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see>
        ///     <cref>{ClientMapperProfile}</cref>
        /// </see>
        /// </summary>
        public IdentityResourceMapperProfile()
        {
            // entity to model
            CreateMap<OrleansIdentityResource, IdentityServer4.Models.IdentityResource>(MemberList.Destination)
                .ForMember(x => x.Properties,
                    opt => opt.MapFrom(src => src.Properties))
                .ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims))
                .ReverseMap();
            // model to entity
        }
    }
}