namespace FitnessProgram.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class BestResultsWorkWithData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestResultPhotos_BestResults_BestResultId",
                table: "BestResultPhotos");

            migrationBuilder.DropColumn(
                name: "ImageUrlAfter",
                table: "BestResults");

            migrationBuilder.DropColumn(
                name: "ImageUrlBefore",
                table: "BestResults");

            migrationBuilder.AlterColumn<int>(
                name: "BestResultId",
                table: "BestResultPhotos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BestResultPhotos_BestResults_BestResultId",
                table: "BestResultPhotos",
                column: "BestResultId",
                principalTable: "BestResults",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BestResultPhotos_BestResults_BestResultId",
                table: "BestResultPhotos");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrlAfter",
                table: "BestResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrlBefore",
                table: "BestResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "BestResultId",
                table: "BestResultPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BestResultPhotos_BestResults_BestResultId",
                table: "BestResultPhotos",
                column: "BestResultId",
                principalTable: "BestResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
