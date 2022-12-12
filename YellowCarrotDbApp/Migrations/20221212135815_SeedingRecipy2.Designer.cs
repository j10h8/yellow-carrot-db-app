﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YellowCarrotDbApp.Data;

#nullable disable

namespace YellowCarrotDbApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221212135815_SeedingRecipy2")]
    partial class SeedingRecipy2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.Property<int>("RecipiesRecipeId")
                        .HasColumnType("int");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("int");

                    b.HasKey("RecipiesRecipeId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("RecipeTag");
                });

            modelBuilder.Entity("YellowCarrotDbApp.Models.AppUser", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Username");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Username = "user"
                        });
                });

            modelBuilder.Entity("YellowCarrotDbApp.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            IngredientId = 1,
                            Name = "Eggs",
                            Quantity = "2",
                            RecipeId = 1
                        },
                        new
                        {
                            IngredientId = 2,
                            Name = "Milk",
                            Quantity = "3 dl",
                            RecipeId = 1
                        },
                        new
                        {
                            IngredientId = 3,
                            Name = "Flour",
                            Quantity = "2 dl",
                            RecipeId = 1
                        },
                        new
                        {
                            IngredientId = 4,
                            Name = "Salt",
                            Quantity = "1 ml",
                            RecipeId = 1
                        },
                        new
                        {
                            IngredientId = 5,
                            Name = "Sugar",
                            Quantity = "0,5 dl",
                            RecipeId = 1
                        },
                        new
                        {
                            IngredientId = 6,
                            Name = "Butter",
                            Quantity = "150 g",
                            RecipeId = 2
                        },
                        new
                        {
                            IngredientId = 7,
                            Name = "Milk",
                            Quantity = "3 dl",
                            RecipeId = 2
                        },
                        new
                        {
                            IngredientId = 8,
                            Name = "Sugar",
                            Quantity = "1,5 dl",
                            RecipeId = 2
                        },
                        new
                        {
                            IngredientId = 9,
                            Name = "Salt",
                            Quantity = "1 ml",
                            RecipeId = 2
                        },
                        new
                        {
                            IngredientId = 10,
                            Name = "Flour",
                            Quantity = "8 dl",
                            RecipeId = 2
                        },
                        new
                        {
                            IngredientId = 11,
                            Name = "Cinnamon",
                            Quantity = "1 tbsp",
                            RecipeId = 2
                        },
                        new
                        {
                            IngredientId = 12,
                            Name = "Yeast",
                            Quantity = "50 g",
                            RecipeId = 2
                        },
                        new
                        {
                            IngredientId = 13,
                            Name = "Egg",
                            Quantity = "1",
                            RecipeId = 2
                        },
                        new
                        {
                            IngredientId = 14,
                            Name = "Crushed loaf sugar",
                            Quantity = "1 tbsp",
                            RecipeId = 2
                        });
                });

            modelBuilder.Entity("YellowCarrotDbApp.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecipeId");

                    b.HasIndex("Username");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            RecipeId = 1,
                            Name = "Pancakes",
                            Username = "user"
                        },
                        new
                        {
                            RecipeId = 2,
                            Name = "Cinnamon buns",
                            Username = "user"
                        });
                });

            modelBuilder.Entity("YellowCarrotDbApp.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("RecipeTag", b =>
                {
                    b.HasOne("YellowCarrotDbApp.Models.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipiesRecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YellowCarrotDbApp.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YellowCarrotDbApp.Models.Ingredient", b =>
                {
                    b.HasOne("YellowCarrotDbApp.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("YellowCarrotDbApp.Models.Recipe", b =>
                {
                    b.HasOne("YellowCarrotDbApp.Models.AppUser", "User")
                        .WithMany("Recipies")
                        .HasForeignKey("Username");

                    b.Navigation("User");
                });

            modelBuilder.Entity("YellowCarrotDbApp.Models.AppUser", b =>
                {
                    b.Navigation("Recipies");
                });

            modelBuilder.Entity("YellowCarrotDbApp.Models.Recipe", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
