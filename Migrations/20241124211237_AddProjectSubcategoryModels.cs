using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTimePredicter.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectSubcategoryModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Quests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "Quests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId",
                table: "Quests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubcategoryId1",
                table: "Quests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    projectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    projectDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.projectId);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    subcategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subcategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    subcategoryDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.subcategoryId);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Categories",
                        principalColumn: "categoryId");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "userId",
                keyValue: 1,
                column: "createdAt",
                value: new DateOnly(2024, 11, 24));

            migrationBuilder.CreateIndex(
                name: "IX_Quests_ProjectId",
                table: "Quests",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_ProjectId1",
                table: "Quests",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_SubcategoryId",
                table: "Quests",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_SubcategoryId1",
                table: "Quests",
                column: "SubcategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId1",
                table: "Subcategories",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Projects_ProjectId",
                table: "Quests",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "projectId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Projects_ProjectId1",
                table: "Quests",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Subcategories_SubcategoryId",
                table: "Quests",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "subcategoryId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Subcategories_SubcategoryId1",
                table: "Quests",
                column: "SubcategoryId1",
                principalTable: "Subcategories",
                principalColumn: "subcategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Projects_ProjectId",
                table: "Quests");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Projects_ProjectId1",
                table: "Quests");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Subcategories_SubcategoryId",
                table: "Quests");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Subcategories_SubcategoryId1",
                table: "Quests");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Quests_ProjectId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_ProjectId1",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_SubcategoryId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_SubcategoryId1",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "SubcategoryId1",
                table: "Quests");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "userId",
                keyValue: 1,
                column: "createdAt",
                value: new DateOnly(2024, 11, 20));
        }
    }
}
