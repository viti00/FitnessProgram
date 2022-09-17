namespace FitnessProgram.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserWorkingWithData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ProfilePictureId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Image_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "Image",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Image_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
