using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicChecklist.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb_11152023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    ChecklistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Createad = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "GETUTCDATE()"),
                    Clocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.ChecklistId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ChecklistId = table.Column<int>(type: "int", nullable: false),
                    Createad = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "GetUtcDate()"),
                    Clocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.IssueId);
                    table.ForeignKey(
                        name: "FK_Issues_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "ChecklistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChecklists",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChecklistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChecklists", x => new { x.UserId, x.ChecklistId });
                    table.ForeignKey(
                        name: "FK_UserChecklists_Checklists_UserId",
                        column: x => x.UserId,
                        principalTable: "Checklists",
                        principalColumn: "ChecklistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChecklists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserIssueStatuses",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIssueStatuses", x => new { x.UserId, x.IssueId });
                    table.ForeignKey(
                        name: "FK_UserIssueStatuses_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserIssueStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ChecklistId",
                table: "Issues",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIssueStatuses_IssueId",
                table: "UserIssueStatuses",
                column: "IssueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChecklists");

            migrationBuilder.DropTable(
                name: "UserIssueStatuses");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Checklists");
        }
    }
}
