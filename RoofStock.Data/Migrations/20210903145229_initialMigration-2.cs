using Microsoft.EntityFrameworkCore.Migrations;

namespace RoofStock.Data.Migrations
{
    public partial class initialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GrossYieldPercentage",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "100.00",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "100",
                oldStored: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GrossYieldPercentage",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "100",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "100.00",
                oldStored: true);
        }
    }
}
