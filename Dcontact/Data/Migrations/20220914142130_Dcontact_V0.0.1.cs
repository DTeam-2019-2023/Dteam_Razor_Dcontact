using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dcontact.Data.Migrations
{
    public partial class Dcontact_V001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "tbOrderInformation",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TradingCode",
                table: "tbOrderInformation",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(450)",
                oldUnicode: false,
                oldMaxLength: 450);

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

            migrationBuilder.AddForeignKey(
                name: "FK_tbDcontact_tbUser",
                table: "tbDcontact",
                column: "ID_User",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbOrderInformation_AspNetUsers",
                table: "tbOrderInformation",
                column: "ID_user",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbReport_AspNetUsers",
                table: "tbReport",
                column: "ID_user",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_tbDcontact_IdTbDcontactNavigationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_tbDcontact_tbUser",
                table: "tbDcontact");

            migrationBuilder.DropForeignKey(
                name: "FK_tbOrderInformation_AspNetUsers",
                table: "tbOrderInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_tbReport_AspNetUsers",
                table: "tbReport");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdTbDcontactNavigationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdTbDcontactNavigationId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "tbOrderInformation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "TradingCode",
                table: "tbOrderInformation",
                type: "varchar(450)",
                unicode: false,
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
