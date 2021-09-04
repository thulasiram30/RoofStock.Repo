using Microsoft.EntityFrameworkCore.Migrations;

namespace RoofStock.Data.Migrations
{
    public partial class initialMigration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GrossYieldPercentage",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "ROUND(([MonthlyRent] * 12 / [ListPrice]) * 100.00, 2)",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "([MonthlyRent] * 12 / [ListPrice]) * 100.00",
                oldStored: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GrossYieldPercentage",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "([MonthlyRent] * 12 / [ListPrice]) * 100.00",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "ROUND(([MonthlyRent] * 12 / [ListPrice]) * 100.00, 2)",
                oldStored: true);
        }
    }
}
