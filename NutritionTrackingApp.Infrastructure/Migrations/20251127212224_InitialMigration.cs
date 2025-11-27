using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NutritionTrackingApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpoonacularId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "food_entries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Calories = table.Column<decimal>(type: "numeric", nullable: false),
                    Protein = table.Column<decimal>(type: "numeric", nullable: false),
                    Carbs = table.Column<decimal>(type: "numeric", nullable: false),
                    Fat = table.Column<decimal>(type: "numeric", nullable: false),
                    Sugar = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_food_entries_ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_food_entries_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ingredients",
                columns: new[] { "Id", "Name", "SpoonacularId" },
                values: new object[,]
                {
                    { 1, "apple", 9000 },
                    { 2, "bread", 9001 },
                    { 3, "beef", 9002 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "PasswordHash", "Username" },
                values: new object[] { 1, new DateTime(2025, 11, 27, 21, 22, 24, 502, DateTimeKind.Utc).AddTicks(619), "$2a$11$b8gfSsf3lTecGBuZoccxbOzj8sl95RUiIm9pPmfvFqNKkfhthC56.", "testuser" });

            migrationBuilder.InsertData(
                table: "food_entries",
                columns: new[] { "Id", "Amount", "Calories", "Carbs", "CreatedAt", "Fat", "IngredientId", "Protein", "Sugar", "Unit", "UserId" },
                values: new object[,]
                {
                    { 1, 150m, 50m, 200m, new DateTime(2025, 11, 27, 21, 22, 24, 502, DateTimeKind.Utc).AddTicks(632), 5m, 1, 10m, 120m, "g", 1 },
                    { 2, 120m, 100m, 250m, new DateTime(2025, 11, 27, 21, 22, 24, 502, DateTimeKind.Utc).AddTicks(637), 10m, 2, 15m, 40m, "g", 1 },
                    { 3, 100m, 120m, 200m, new DateTime(2025, 11, 27, 21, 22, 24, 502, DateTimeKind.Utc).AddTicks(639), 60m, 3, 90m, 5m, "g", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_food_entries_IngredientId",
                table: "food_entries",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_food_entries_UserId",
                table: "food_entries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ingredients_SpoonacularId",
                table: "ingredients",
                column: "SpoonacularId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "food_entries");

            migrationBuilder.DropTable(
                name: "ingredients");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
