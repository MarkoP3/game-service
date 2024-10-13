using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GameService.Infrastructure;

public sealed class GameDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Choice> Choices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
