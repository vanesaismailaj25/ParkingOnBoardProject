using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingOnBoard.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCityNameToCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "Cities",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Cities",
                newName: "CityName");
        }
    }
}
