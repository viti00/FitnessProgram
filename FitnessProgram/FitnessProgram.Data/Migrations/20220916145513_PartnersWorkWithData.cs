namespace FitnessProgram.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class PartnersWorkWithData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_AspNetUsers_UserId",
                table: "ProfilePhotos");

            migrationBuilder.DropIndex(
                name: "IX_ProfilePhotos_UserId",
                table: "ProfilePhotos");

            migrationBuilder.DropIndex(
                name: "IX_PartnerPhotos_PartnerId",
                table: "PartnerPhotos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProfilePhotos");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Partners");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPhotos_PartnerId",
                table: "PartnerPhotos",
                column: "PartnerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PartnerPhotos_PartnerId",
                table: "PartnerPhotos");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProfilePhotos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhotos_UserId",
                table: "ProfilePhotos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPhotos_PartnerId",
                table: "PartnerPhotos",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_AspNetUsers_UserId",
                table: "ProfilePhotos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
