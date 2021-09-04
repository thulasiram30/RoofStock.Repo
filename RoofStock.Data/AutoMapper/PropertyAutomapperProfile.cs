using AutoMapper;
using RoofStock.Domain;

namespace RoofStock.Data.AutoMapper
{
    public class PropertyAutomapperProfile: Profile
    {
        public PropertyAutomapperProfile()
        {
            CreateMap<Property, DTO.PropertyResponse>();
            CreateMap<DTO.PropertyRequest, Property>().ForMember(p => p.Id, s => s.Ignore());
            CreateMap<Address, DTO.AddressResponse>();
            CreateMap<DTO.AddressRequest, Address>();
        }
    }
}
