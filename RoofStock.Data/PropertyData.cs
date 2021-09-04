using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RoofStock.Data.Contracts;
using RoofStock.Data.DataContexts;
using RoofStock.Data.DTO;
using RoofStock.Data.Util;
using RoofStock.Util;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStock.Data
{
    public class PropertyData : IPropertyData
    {
        private readonly RoofStockDBContext _roofStockDBContext;
        private readonly IMapper _mapper;
        public PropertyData(RoofStockDBContext roofStockDBContext, IMapper mapper)
        {
            _roofStockDBContext = roofStockDBContext;
            _mapper = mapper;
        }

        public async Task<PagedResponse<PropertyResponse>> GetProperties(PagedRequest pagedRequest)
        {

            return await _roofStockDBContext.Properties.OrderBy(property => property.Id)
                                            .GetPagedAsync<Domain.Property, PropertyResponse>(pagedRequest, _mapper);
        }

        public async Task<PropertyResponse> GetProperty(int id)
        {
            return await _roofStockDBContext.Properties.Where(property => property.Id == id)
                                                        .ProjectTo<PropertyResponse>(_mapper.ConfigurationProvider)
                                                        .FirstOrDefaultAsync();
        }

        public async Task<PropertyResponse> AddProperty(PropertyRequest propertyRequest)
        {
            if (propertyRequest.Id > 0 && await _roofStockDBContext.Properties.AnyAsync(prop => prop.Id != propertyRequest.Id))
                throw new DuplicateEntityException($"Property already exist for Id: { propertyRequest.Id}");

            var property = _mapper.Map<Domain.Property>(propertyRequest);
            _roofStockDBContext.Properties.Add(property);
            await _roofStockDBContext.SaveChangesAsync();
            return await GetProperty(property.Id);
        }

        public async Task<PropertyResponse> UpdateProperty(PropertyRequest propertyRequest)
        {
            var property = await _roofStockDBContext.Properties.FirstOrDefaultAsync(prop => prop.Id == propertyRequest.Id);
            if (property is null)
                throw new EntityNotFoundException($"Property not found for Id: { propertyRequest.Id}");

            _mapper.Map(propertyRequest, property);
            await _roofStockDBContext.SaveChangesAsync();
            return await GetProperty(property.Id);
        }
    }
}
