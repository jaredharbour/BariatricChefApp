using BariatricChefApp.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace BariatricChefApp.Core.Data;

public class BariatricChefDbContext : DbContext
{
    public BariatricChefDbContext(DbContextOptions<BariatricChefDbContext> options) : base(options) { }

    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("BariatricChef");

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.ToContainer("Recipes");
            entity.HasPartitionKey(r => r.Stage);
            entity.HasKey(r => r.Id);

            entity.OwnsOne(r => r.NutritionInfo);
            entity.OwnsMany(r => r.Ingredients, ingredient =>
            {
                ingredient.OwnsOne(i => i.NutritionInfo);
            });
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.ToContainer("UserProfiles");
            entity.HasPartitionKey(p => p.UserId);
            entity.HasKey(p => p.Id);
        });
    }
}
