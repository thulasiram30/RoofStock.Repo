using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RoofStock.Data.DTO;
using RoofStock.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoofStock.Data.SeedData
{
    public class RoofStockSeedData : IRoofStockSeedData
    {
        private readonly HttpClient _roofStockClient;
        private readonly IMapper _mapper;
        public RoofStockSeedData(HttpClient roofStockClient, IMapper mapper)
        {
            _roofStockClient = roofStockClient;
            _mapper = mapper;
        }

        public IEnumerable<Domain.Property> SeedData()
        {
            var propertiesResult = new List<Domain.Property>();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://samplerspubcontent.blob.core.windows.net/public/properties.json"),
                Method = HttpMethod.Get
            };
            var response =  _roofStockClient.Send(request);
            var result = response.Content.ReadAsStringAsync().Result;
            dynamic prp = JsonConvert.DeserializeObject(result);
            foreach (var prop in prp.properties)
            {
                propertiesResult.Add(new Domain.Property()
                {
                    Address = new Domain.Address()
                    {
                        Address1 = prop.address.address1,
                        Address2 = prop.address.address2,
                        City = prop.address.city,
                        Country = prop.address.country,
                        County = prop.address.county,
                        District = prop.address.district,
                        State = prop.address.state,
                        Zip = prop.address.zip != null ? (int)prop.address.zip : null,
                        ZipPlus4 = prop.address.zipPlus4 != null ? (int)prop.address.zipPlus4 : null,
                        CreatedOn = DateTime.UtcNow,
                        ModifiedOn = DateTime.UtcNow
                    },
                    ListPrice = prop.financial != null ? (decimal)prop.financial.listPrice : 0,
                    MonthlyRent = prop.financial != null ? (decimal)prop.financial.monthlyRent : 0,
                    YearBuilt = (short)(prop.physical != null ? prop.physical.yearBuilt : 0),
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow
                });
            }
            return propertiesResult.Where(p => p.ListPrice > 0);            
        }
    }
}
