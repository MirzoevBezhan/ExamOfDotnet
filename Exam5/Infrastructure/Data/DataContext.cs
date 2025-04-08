using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasMany(u => u.Bookings)
        .WithOne(b => b.User)
        .HasForeignKey(b => b.UserId);


        modelBuilder.Entity<User>()
        .HasIndex(u => u.Username)
        .IsUnique();

        modelBuilder.Entity<Car>()
        .HasMany(c => c.Bookings)
        .WithOne(B => B.Car)
        .HasForeignKey(c => c.CarId);

        modelBuilder.Entity<Car>()
        .Property(c => c.PricePerDay > 0)
        .IsRequired();

        modelBuilder.Entity<Booking>()
        .Property(b => b.EndDate > b.StartDate)
        .IsRequired();

    }


}
