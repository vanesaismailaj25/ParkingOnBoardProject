using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ParkingOnBoard.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Property_TotalValidSlots_FromStreet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalValidSlots",
                table: "Streets");

            migrationBuilder.InsertData(
                table: "ParkingSlots",
                columns: new[] { "Id", "IsBusy", "IsClosed", "SlotNumber", "StreetId", "StreetName" },
                values: new object[,]
                {
                    { 3, false, false, 3, 3, "Antipatrea" },
                    { 4, false, false, 4, 3, "Antipatrea" },
                    { 5, false, false, 5, 3, "Antipatrea" },
                    { 6, false, false, 6, 3, "Antipatrea" },
                    { 7, false, false, 7, 2, "Sami Frasheri" },
                    { 8, false, false, 8, 2, "Sami Frasheri" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkingSlots",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkingSlots",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkingSlots",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ParkingSlots",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ParkingSlots",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ParkingSlots",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AddColumn<int>(
                name: "TotalValidSlots",
                table: "Streets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 1,
                column: "TotalValidSlots",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 2,
                column: "TotalValidSlots",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 3,
                column: "TotalValidSlots",
                value: 4);
        }
    }
}
