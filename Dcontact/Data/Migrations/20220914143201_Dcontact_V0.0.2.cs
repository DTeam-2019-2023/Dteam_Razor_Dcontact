using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dcontact.Data.Migrations
{
    public partial class Dcontact_V002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tbDcontact_IdTbDcontactNavigationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdTbDcontactNavigationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdTbDcontactNavigationId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdTbDcontactNavigationId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdTbDcontactNavigationId",
                table: "AspNetUsers",
                column: "IdTbDcontactNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_tbDcontact_IdTbDcontactNavigationId",
                table: "AspNetUsers",
                column: "IdTbDcontactNavigationId",
                principalTable: "tbDcontact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
