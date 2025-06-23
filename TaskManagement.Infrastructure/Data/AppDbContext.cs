
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagement.Core.Models;

namespace TaskManagement.Infrastructure.Data;

public class AppDbContext : DbContext
{

    public DbSet<UserItem> Users { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserItem>()
            .HasIndex(u => u.Name)
            .IsUnique();

        modelBuilder.Entity<TaskItem>()
            .HasIndex(t => t.Title)
            .IsUnique();

        var guidListConverter = new ValueConverter<List<Guid>, string>(
        list => string.Join(',', list),
        str => str.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
        );

        var guidListComparer = new ValueComparer<List<Guid>>(
            (l1, l2) => l1.SequenceEqual(l2),
            l => l.Aggregate(0, (hash, guid) => HashCode.Combine(hash, guid.GetHashCode())),
            l => l.ToList()
        );

        modelBuilder.Entity<TaskItem>()
            .Property(t => t.AssignmentHistory)
            .HasConversion(guidListConverter)
            .Metadata.SetValueComparer(guidListComparer);
    }
}
