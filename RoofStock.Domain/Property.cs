using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoofStock.Domain
{
    public class Property: Base
    {
        [Required]
        public Address Address { get; set; }
        [Required]
        public short YearBuilt { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ListPrice { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyRent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal GrossYieldPercentage { get; set; }
    }
}
