using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceCourse.Data.Migrations
{
    public partial class PersonalAdvice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalAdvice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentPageId = table.Column<int>(type: "int", nullable: false),
                    PersonalityType = table.Column<int>(type: "int", nullable: false),
                    advice = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalAdvice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalAdvice_CoursePages_ParentPageId",
                        column: x => x.ParentPageId,
                        principalTable: "CoursePages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalAdvice_ParentPageId",
                table: "PersonalAdvice",
                column: "ParentPageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalAdvice");
        }
    }
}
