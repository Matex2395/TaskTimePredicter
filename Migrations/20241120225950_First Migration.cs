using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTimePredicter.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    categoryDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__23CAF1D81A434EC4", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    userEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    userRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Developer"),
                    createdAt = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__CB9A1CFF1714BBE7", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    taskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    questName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    estimatedTime = table.Column<double>(type: "float", nullable: false),
                    actualTime = table.Column<double>(type: "float", nullable: true),
                    questState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "In Progress"),
                    creationDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    categoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryId1 = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.taskId);
                    table.ForeignKey(
                        name: "FK_Quests_Categories_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Categories",
                        principalColumn: "categoryId");
                    table.ForeignKey(
                        name: "FK_Quests_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Quests_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK_Quests_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "userId", "createdAt", "userEmail", "userName", "userPassword", "userRole" },
                values: new object[] { 1, new DateOnly(2024, 11, 20), "admin@gmail.com", "Administrador Base", "admin123", "Administrator" });

            migrationBuilder.CreateIndex(
                name: "IX_Quests_categoryId",
                table: "Quests",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_CategoryId1",
                table: "Quests",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_userId",
                table: "Quests",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_UserId1",
                table: "Quests",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__D54ADF555E66784B",
                table: "Users",
                column: "userEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
