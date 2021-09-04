using RoofStock.Data.DTO;
using System.Threading.Tasks;

namespace RoofStock.Data.Contracts
{
    public interface IPropertyData
    {
        Task<PropertyResponse> AddProperty(PropertyRequest propertyRequest);
        Task<PagedResponse<PropertyResponse>> GetProperties(PagedRequest pagedRequest);
        Task<PropertyResponse> GetProperty(int id);
        Task<PropertyResponse> UpdateProperty(PropertyRequest propertyRequest);
    }
}