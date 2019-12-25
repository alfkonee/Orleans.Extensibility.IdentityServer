using AutoMapper;
using Orleans.Extensibility.IdentityServer.Grains;

namespace Orleans.Extensibility.IdentityServer.Mappers
{
    /// <summary>
    /// AutoMapper configuration for OrleansIdentityResource
    /// Between model and entity
    /// </summary>
    internal class ApiResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see>
        ///     <cref>{ClientMapperProfile}</cref>
        /// </see>
        /// </summary>
        public ApiResourceMapperProfile()
        {
            // entity to model
            CreateMap<OrleansApiResource, IdentityServer4.Models.ApiResource>(MemberList.Destination)
                .ForMember(x => x.ApiSecrets,opt => opt.MapFrom(src => src.ApiSecrets))
                .ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims))
                .ForMember(x => x.Scopes, opt => opt.MapFrom(src => src.Scopes))
                .ReverseMap();
            CreateMap<IdentityServer4.Models.Secret, OrleansSecret>(MemberList.Source)
                ;
            CreateMap<IdentityServer4.Models.Scope, OrleansScope>(MemberList.Source)
                .ForMember(c=> c.UserClaims, opt=>opt.MapFrom(k=>k.UserClaims))
                .ReverseMap();

            // model to entity
        }
    }
}