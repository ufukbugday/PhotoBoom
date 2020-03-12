using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoBoom.DataAccess.Migrations
{
    public partial class TagPhotoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Photos_PhotoId",
                table: "Tags");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoId",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Photos_PhotoId",
                table: "Tags",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Photos_PhotoId",
                table: "Tags");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoId",
                table: "Tags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Photos_PhotoId",
                table: "Tags",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
