using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ParkingOnBoard.Migrations
{
    /// <inheritdoc />
    public partial class DBInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ParkingSlots",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "Streets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "ParkingSlots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityName" },
                values: new object[,]
                {
                    { 1, "Tirana" },
                    { 2, "Berat" },
                    { 3, "Skrapar" }
                });

            migrationBuilder.InsertData(
                table: "Streets",
                columns: new[] { "Id", "CityId", "CityName", "HasTwoSides", "IsClosed", "Name", "TotalValidSlots" },
                values: new object[,]
                {
                    { 1, 1, "Tirana", false, false, "Naim Frasheri", 2 },
                    { 2, 1, "Tirana", false, false, "Sami Frasheri", 2 },
                    { 3, 2, "Berat", true, false, "Antipatrea", 4 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSlots",
                columns: new[] { "Id", "IsBusy", "IsClosed", "SlotNumber", "StreetId", "StreetName" },
                values: new object[,]
                {
                    { 1, false, false, 1, 1, "Naim Frasheri" },
                    { 2, false, false, 2, 1, "Naim Frasheri" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingSlots",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkingSlots",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "ParkingSlots");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ParkingSlots",
                newName: "ID");
        }
    }
}
