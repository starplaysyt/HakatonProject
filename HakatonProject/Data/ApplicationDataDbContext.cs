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
        
        if (!Database.EnsureCreated())
        {
            _logger.LogError("Database ensured already created.");
        }
        
        _logger.LogTrace("ApplicationDataDbContext created.");
    }
}