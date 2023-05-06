using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.NewGuid(), Admin = true, CreatedBy = "Admin", Login = "Admin", Password = "Admin123",
            CreatedOn = DateTime.Today, Name = "Admin", Gender = 2
        });
    }
}