using BepNhaMinh.Models;
using Microsoft.EntityFrameworkCore;

namespace BepNhaMinh.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<CookingStep> CookingSteps => Set<CookingStep>();
    public DbSet<Favorite> Favorites => Set<Favorite>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(x => x.Recipe).WithMany(x => x.RecipeIngredients)
            .HasForeignKey(x => x.RecipeId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(x => x.Ingredient).WithMany(x => x.RecipeIngredients)
            .HasForeignKey(x => x.IngredientId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<CookingStep>()
            .HasOne(x => x.Recipe).WithMany(x => x.CookingSteps)
            .HasForeignKey(x => x.RecipeId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Favorite>()
            .HasOne(x => x.Recipe).WithMany(x => x.Favorites)
            .HasForeignKey(x => x.RecipeId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Favorite>()
            .HasIndex(x => new { x.RecipeId, x.ClientKey }).IsUnique();
    }
}
