using HakatonProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HakatonProject.Data;

public class ApplicationDataDbContext : DbContext
{
    private readonly ILogger _logger;

    public ApplicationDataDbContext(DbContextOptions<ApplicationDataDbContext> options, ILogger logger) : base(options)
    {
        _logger = logger;

        _logger.LogTrace("ApplicationDataDbContext created.");
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserContact> UserContacts { get; set; }
    public DbSet<UserEvent> UserEvents { get; set; }
    public DbSet<UserInterest> UserInterests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}