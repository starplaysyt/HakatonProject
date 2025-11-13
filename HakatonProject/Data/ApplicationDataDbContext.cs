using Microsoft.EntityFrameworkCore;
using HakatonProject.Models;

namespace HakatonProject.Data;

public class ApplicationDataDbContext : DbContext
{
    private readonly ILogger _logger;
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserContact> UserContacts { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<Faculty> Faculties { get; set; }

    public ApplicationDataDbContext(DbContextOptions<ApplicationDataDbContext> options, ILogger logger) : base(options)
    {
        _logger = logger;
        
        _logger.LogTrace("ApplicationDataDbContext created.");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Interests)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Events)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Contacts)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserFaculty)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Event>()
            .HasMany(u => u.Visitors)
            .WithOne()
            .OnDelete(DeleteBehavior.SetNull);
    }
}