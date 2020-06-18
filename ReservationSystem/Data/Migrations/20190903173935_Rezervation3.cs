using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationSystem.Data.Migrations
{
    public partial class Rezervation3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Rezervations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Rezervations");
        }
    }
}
