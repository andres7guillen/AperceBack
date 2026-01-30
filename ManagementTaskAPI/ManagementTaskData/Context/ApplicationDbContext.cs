using ManagementTaskDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementTaskData.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureUser(modelBuilder);
        ConfigureTaskItem(modelBuilder);
    }

    private static void ConfigureUser(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();

        user.HasKey(u => u.Id);

        user.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        user.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        user.HasIndex(u => u.Email)
            .IsUnique();

        user.HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .IsRequired();
    }

    private static void ConfigureTaskItem(ModelBuilder modelBuilder)
    {
        var task = modelBuilder.Entity<TaskItem>();

        task.HasKey(t => t.Id);

        task.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        task.Property(t => t.Status)
            .IsRequired()
            .HasConversion<int>();

        task.HasOne(t => t.User)            
        .WithMany(u => u.Tasks)
        .HasForeignKey(t => t.UserId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Cascade);
    }
}

