using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurfsupEmil.Migrations
{
    /// <inheritdoc />
    public partial class removedsomething : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyPrice",
                table: "Surfboards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "HourlyPrice",
                table: "Surfboards",
                type: "float",
                nullable: true);
        }
    }
}
