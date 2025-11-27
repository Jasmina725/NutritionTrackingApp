using Microsoft.EntityFrameworkCore;
using NutritionTrackingApp.Core.Entities;
using NutritionTrackingApp.Infrastructure.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTrackingApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<FoodEntry> FoodEntries => Set<FoodEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("users");
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.Username).IsUnique();
                b.Property(x => x.Username).IsRequired();
                b.Property(x => x.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Ingredient>(b =>
            {
                b.ToTable("ingredients");
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.SpoonacularId).IsUnique();
                b.Property(x => x.SpoonacularId).IsRequired();
                b.Property(x => x.Name).IsRequired();
            });

            modelBuilder.Entity<FoodEntry>(b =>
            {
                b.ToTable("food_entries");
                b.HasKey(x => x.Id);

                b.Property(x => x.Amount).IsRequired();
                b.Property(x => x.Unit).IsRequired();
                b.Property(x => x.CreatedAt).HasDefaultValueSql("now()");

                b.Property(x => x.Calories).IsRequired();
                b.Property(x => x.Protein).IsRequired();
                b.Property(x => x.Carbs).IsRequired();
                b.Property(x => x.Fat).IsRequired();
                b.Property(x => x.Sugar).IsRequired();

                b.HasOne(x => x.User)
                    .WithMany(u => u.FoodEntries)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.Ingredient)
                    .WithMany(i => i.FoodEntries)
                    .HasForeignKey(x => x.IngredientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            IEnumerable<User> users = new List<User>
            {
                new User() { Id = 1, Username = "testuser", PasswordHash = "$2a$11$b8gfSsf3lTecGBuZoccxbOzj8sl95RUiIm9pPmfvFqNKkfhthC56.", CreatedAt = DateTime.UtcNow },
            };

            IEnumerable<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient() { Id = 1, SpoonacularId = 9000, Name = "apple" },
                new Ingredient() { Id = 2, SpoonacularId = 9001, Name = "bread" },
                new Ingredient() { Id = 3, SpoonacularId = 9002, Name = "beef" },
            };

            IEnumerable<FoodEntry> entries = new List<FoodEntry>
            {
                new FoodEntry() { Id = 1, UserId = 1, IngredientId = 1, Amount = 150, Unit = "g", CreatedAt = DateTime.UtcNow,
                Calories = 50, Protein = 10, Carbs = 200, Fat = 5, Sugar = 120},
                new FoodEntry() { Id = 2, UserId = 1, IngredientId = 2, Amount = 120, Unit = "g", CreatedAt = DateTime.UtcNow,
                Calories = 100, Protein = 15, Carbs = 250, Fat = 10, Sugar = 40},
                new FoodEntry() { Id = 3, UserId = 1, IngredientId = 3, Amount = 100, Unit = "g", CreatedAt = DateTime.UtcNow,
                Calories = 120, Protein = 90, Carbs = 200, Fat = 60, Sugar = 5},
            };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Ingredient>().HasData(ingredients);
            modelBuilder.Entity<FoodEntry>().HasData(entries);
        }
    }
}
