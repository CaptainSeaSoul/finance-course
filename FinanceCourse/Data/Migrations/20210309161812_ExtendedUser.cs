using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceCourse.Data.Migrations
{
    public partial class ExtendedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoursePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentCourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskTool = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursePages_Courses_ParentCourseId",
                        column: x => x.ParentCourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursePageFinanceCourseUser",
                columns: table => new
                {
                    CompletedPagesId = table.Column<int>(type: "int", nullable: false),
                    PassedUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePageFinanceCourseUser", x => new { x.CompletedPagesId, x.PassedUsersId });
                    table.ForeignKey(
                        name: "FK_CoursePageFinanceCourseUser_AspNetUsers_PassedUsersId",
                        column: x => x.PassedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursePageFinanceCourseUser_CoursePages_CompletedPagesId",
                        column: x => x.CompletedPagesId,
                        principalTable: "CoursePages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursePageFinanceCourseUser_PassedUsersId",
                table: "CoursePageFinanceCourseUser",
                column: "PassedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePages_ParentCourseId",
                table: "CoursePages",
                column: "ParentCourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursePageFinanceCourseUser");

            migrationBuilder.DropTable(
                name: "CoursePages");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
