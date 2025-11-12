using Microsoft.EntityFrameworkCore;

namespace HakatonProject.Data;

public class ApplicationDataDbContext : DbContext
{
    private readonly ILogger _logger;
    
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