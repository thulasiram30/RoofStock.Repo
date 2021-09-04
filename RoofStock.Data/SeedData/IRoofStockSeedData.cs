using RoofStock.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoofStock.Data.SeedData
{
    public interface IRoofStockSeedData
    {
        IEnumerable<Domain.Property> SeedData();
    }
}