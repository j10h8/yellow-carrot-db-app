using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YellowCarrotDbApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_Recipes_AppUsers_Username",
                        column: x => x.Username,
                        principalTable: "AppUsers",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipiesRecipeId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipiesRecipeId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipiesRecipeId",
                        column: x => x.RecipiesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                column: "Username",
                value: "user");

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "Name", "Username" },
                values: new object[,]
                {
                    { 1, "Pancakes", "user" },
                    { 2, "Cinnamon buns", "user" },
                    { 3, "Tomato soup", "user" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Name", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 1, "Eggs", "2", 1 },
                    { 2, "Milk", "3 dl", 1 },
                    { 3, "Flour", "2 dl", 1 },
                    { 4, "Salt", "1 ml", 1 },
                    { 5, "Sugar", "0,5 dl", 1 },
                    { 6, "Butter", "150 g", 2 },
                    { 7, "Milk", "3 dl", 2 },
                    { 8, "Sugar", "1,5 dl", 2 },
                    { 9, "Salt", "1 ml", 2 },
                    { 10, "Flour", "8 dl", 2 },
                    { 11, "Cinnamon", "1 tbsp", 2 },
                    { 12, "Yeast", "50 g", 2 },
                    { 13, "Egg", "1", 2 },
                    { 14, "Crushed loaf sugar", "1 tbsp", 2 },
                    { 15, "Onion", "1", 3 },
                    { 16, "Crushed tomatoes", "1 kg", 3 },
                    { 17, "Tomato purée", "2 tbsp", 3 },
                    { 18, "Vegetable broth", "7,5 dl", 3 },
                    { 19, "Balsamic vinegar", "0,5 tbsp", 3 },
                    { 20, "Cinnamon", "1 tbsp", 3 },
                    { 21, "Oregano", "0,5 tbsp", 3 },
                    { 22, "Salt", "1 ml", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_Username",
                table: "Recipes",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagsTagId",
                table: "RecipeTag",
                column: "TagsTagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
