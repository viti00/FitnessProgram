using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessProgram.Data.Migrations
{
    public partial class CustomersIsApprovedColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Customers");
        }
    }
}
