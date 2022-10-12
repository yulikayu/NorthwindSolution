using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Persistence.Migrations
{
    public partial class AddRoleToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "001ee931-cd04-4b8d-809f-f700a0377ce0", "438b325d-b515-4be2-a227-ba9d470e6f23", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "263cb47b-5cd1-4646-a57e-dfc2b8634236", "8a230407-908a-419e-90cd-35e6e89042df", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "001ee931-cd04-4b8d-809f-f700a0377ce0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "263cb47b-5cd1-4646-a57e-dfc2b8634236");
        }
    }
}
