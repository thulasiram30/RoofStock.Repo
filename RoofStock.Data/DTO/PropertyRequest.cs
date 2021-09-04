using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofStock.Data.DTO
{
    public class PropertyRequest: IValidatableObject
    {
        public int Id { get; set; }
        public decimal ListPrice { get; set; }
        public decimal MonthlyRent { get; set; }
        public short YearBuilt { get; set; }
        public AddressRequest Address { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ListPrice <= 0)
                yield return new ValidationResult("ListPrice must be greater than zero.");
            if (MonthlyRent <= 0)
                yield return new ValidationResult("MonthlyRent must be greater than zero.");
        }
    }
}
