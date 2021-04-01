using Microsoft.EntityFrameworkCore.Migrations;

namespace FinanceCourse.Data.Migrations
{
    public partial class ToolModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursePageFinanceCourseUser");

            migrationBuilder.CreateTable(
                name: "CoursePageModelFinanceCourseUser",
                columns: table => new
                {
                    CompletedPagesId = table.Column<int>(type: "int", nullable: false),
                    PassedUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePageModelFinanceCourseUser", x => new { x.CompletedPagesId, x.PassedUsersId });
                    table.ForeignKey(
                        name: "FK_CoursePageModelFinanceCourseUser_AspNetUsers_PassedUsersId",
                        column: x => x.PassedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursePageModelFinanceCourseUser_CoursePages_CompletedPagesId",
                        column: x => x.CompletedPagesId,
                        principalTable: "CoursePages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    ToolDataStr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinanceCourseUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tools_AspNetUsers_FinanceCourseUserId",
                        column: x => x.FinanceCourseUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursePageModelFinanceCourseUser_PassedUsersId",
                table: "CoursePageModelFinanceCourseUser",
                column: "PassedUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_FinanceCourseUserId",
                table: "Tools",
                column: "FinanceCourseUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursePageModelFinanceCourseUser");

            migrationBuilder.DropTable(
                name: "Tools");

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
        }
    }
}
