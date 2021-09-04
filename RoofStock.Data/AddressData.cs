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
    public class AddressData : IAddressData
    {
        private readonly RoofStockDBContext _roofStockDBContext;
        private readonly IMapper _mapper;
        public AddressData(RoofStockDBContext roofStockDBContext, IMapper mapper)
        {
            _roofStockDBContext = roofStockDBContext;
            _mapper = mapper;
        }

        public async Task<AddressResponse> GetAddressByPropertyID(int id)
        {
            return await _roofStockDBContext.Properties.Where(property => property.Id == id)
                                                        .Select(property => property.Address)
                                                        .ProjectTo<AddressResponse>(_mapper.ConfigurationProvider)
                                                        .FirstOrDefaultAsync();
        }
    }
}
