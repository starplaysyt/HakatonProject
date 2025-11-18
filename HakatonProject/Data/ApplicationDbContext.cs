using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HakatonProject.Data;

public class ApplicationAuthDbContext : IdentityDbContext
{
    public ApplicationAuthDbContext(DbContextOptions<ApplicationAuthDbContext> options)
        : base(options)
    {
        Console.WriteLine(Database.EnsureCreated());
    }
}