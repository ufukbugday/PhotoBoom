using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoBoom.DataAccess.Migrations
{
    public partial class SeedPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "PhotoPath", "Tag", "Title" },
                values: new object[] { 1, "PhotoPath", "Tag", "Title" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
