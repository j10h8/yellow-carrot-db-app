using Microsoft.EntityFrameworkCore;
using YellowCarrotDbApp.Models;

namespace YellowCarrotDbApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasData(new AppUser()
            {
                Username = "user"
            });

            // ------------------------------------PANCAKES------------------------------------
            modelBuilder.Entity<Recipe>().HasData(new Recipe()
            {
                RecipeId = 1,
                Name = "Pancakes",
                Username = "user"
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 1,
                Name = "Eggs",
                Quantity = "2",
                RecipeId = 1
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 2,
                Name = "Milk",
                Quantity = "3 dl",
                RecipeId = 1
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 3,
                Name = "Flour",
                Quantity = "2 dl",
                RecipeId = 1
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 4,
                Name = "Salt",
                Quantity = "1 ml",
                RecipeId = 1
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 5,
                Name = "Sugar",
                Quantity = "0,5 dl",
                RecipeId = 1
            });

            // -------------------------------CINNAMON BUNS------------------------------------
            modelBuilder.Entity<Recipe>().HasData(new Recipe()
            {
                RecipeId = 2,
                Name = "Cinnamon buns",
                Username = "user"
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 6,
                Name = "Butter",
                Quantity = "150 g",
                RecipeId = 2
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 7,
                Name = "Milk",
                Quantity = "3 dl",
                RecipeId = 2
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 8,
                Name = "Sugar",
                Quantity = "1,5 dl",
                RecipeId = 2
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 9,
                Name = "Salt",
                Quantity = "1 ml",
                RecipeId = 2
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 10,
                Name = "Flour",
                Quantity = "8 dl",
                RecipeId = 2
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 11,
                Name = "Cinnamon",
                Quantity = "1 tbsp",
                RecipeId = 2
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 12,
                Name = "Yeast",
                Quantity = "50 g",
                RecipeId = 2
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 13,
                Name = "Egg",
                Quantity = "1",
                RecipeId = 2
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 14,
                Name = "Crushed loaf sugar",
                Quantity = "1 tbsp",
                RecipeId = 2
            });

            // ---------------------------------TOMATO SOUP------------------------------------
            modelBuilder.Entity<Recipe>().HasData(new Recipe()
            {
                RecipeId = 3,
                Name = "Tomato soup",
                Username = "user"
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 15,
                Name = "Onion",
                Quantity = "1",
                RecipeId = 3
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 16,
                Name = "Crushed tomatoes",
                Quantity = "1 kg",
                RecipeId = 3
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 17,
                Name = "Tomato purée",
                Quantity = "2 tbsp",
                RecipeId = 3
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 18,
                Name = "Vegetable broth",
                Quantity = "7,5 dl",
                RecipeId = 3
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 19,
                Name = "Balsamic vinegar",
                Quantity = "0,5 tbsp",
                RecipeId = 3
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 20,
                Name = "Cinnamon",
                Quantity = "1 tbsp",
                RecipeId = 3
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 21,
                Name = "Oregano",
                Quantity = "0,5 tbsp",
                RecipeId = 3
            });

            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 22,
                Name = "Salt",
                Quantity = "1 ml",
                RecipeId = 3
            });
        }
    }
}
