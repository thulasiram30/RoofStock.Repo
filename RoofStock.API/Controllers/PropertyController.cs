using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RoofStock.Data.Contracts;
using RoofStock.Data.DTO;
using System.Threading.Tasks;

namespace RoofStock.API.Controllers
{
    [EnableCors("AllowOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : Controller
    {
        private readonly IPropertyData _propertyData;
        private readonly IAddressData _addressData;
        public PropertyController(IPropertyData propertyData, IAddressData addressData)
        {
            _propertyData = propertyData;
            _addressData = addressData;
        }
        
        [HttpGet]
        public async Task<PagedResponse<PropertyResponse>> Property([FromQuery]PagedRequest pagedRequest)
        {
            return await _propertyData.GetProperties(pagedRequest);
        }

        [HttpGet("{id}")]
        public async Task<PropertyResponse> Property(int id)
        {
            return await _propertyData.GetProperty(id);
        }

        [HttpPost]
        public async Task<PropertyResponse> Property([FromBody] PropertyRequest property)
        {
            return await _propertyData.AddProperty(property);
        }

        [HttpPut("{id}")]
        public async Task<PropertyResponse> Property(int id, [FromBody] PropertyRequest property)
        {
            property.Id = id;
            return await _propertyData.UpdateProperty(property);
        }

        [HttpGet("{id}/Address")]
        public async Task<AddressResponse> Address(int id)
        {
            return await _addressData.GetAddressByPropertyID(id);
        }
    }
}
