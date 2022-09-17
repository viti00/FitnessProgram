namespace FitnessProgram.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class PostsStartWorkWithData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "Photos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PostId",
                table: "Photos",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Posts_PostId",
                table: "Photos",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Posts_PostId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_PostId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
