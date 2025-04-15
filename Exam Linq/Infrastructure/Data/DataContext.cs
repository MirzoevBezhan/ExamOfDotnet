
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users  { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Domain.Entites.Task> Tasks { get; set; }
    public DbSet<TaskAssignment> TaskAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Entites.Task>()
        .HasOne(t => t.User)
        .WithMany(t => t.Tasks)
        .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<Domain.Entites.Task>()
        .HasOne(t => t.Project)
        .WithMany(t => t.Tasks)
        .HasForeignKey(t => t.ProjectId);

        modelBuilder.Entity<TaskAssignment>()
        .HasOne(t => t.Task)
        .WithMany(t => t.TaskAssignments)
        .HasForeignKey(t => t.TaskId);

        modelBuilder.Entity<TaskAssignment>()
        .HasOne(t => t.User)
        .WithMany(t => t.TaskAssignments)
        .HasForeignKey(t => t.UserId);

    }

}