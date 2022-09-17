namespace FitnessProgram.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MadeProfilePhotoFieldsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_AspNetUsers_UserId",
                table: "ProfilePhotos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProfilePhotos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_AspNetUsers_UserId",
                table: "ProfilePhotos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_AspNetUsers_UserId",
                table: "ProfilePhotos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProfilePhotos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_AspNetUsers_UserId",
                table: "ProfilePhotos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
