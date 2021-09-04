using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofStock.Data.DTO
{
    public class PropertyResponse: BaseResponse
    {
        public decimal ListPrice { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal GrossYieldPercentage { get; set; }
        public short YearBuilt { get; set; }
    }
}
