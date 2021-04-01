using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceCourse.Data.Migrations
{
    public partial class ToolModelFacilitated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToolDataStr",
                table: "Tools",
                newName: "ToolDataJson");

            migrationBuilder.AddColumn<int>(
                name: "PersonalityType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalityType",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ToolDataJson",
                table: "Tools",
                newName: "ToolDataStr");
        }
    }
}
