using HakatonProject.Data;
using HakatonProject.Models;
using Microsoft.EntityFrameworkCore;

public class InterestRepository
{
    private readonly ApplicationDataDbContext _dbContext;

    public InterestRepository(ApplicationDataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Interest?> GetInterestById(long id) =>
        await _dbContext.Interests.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Interest[]> GetAllInterests() =>
        await _dbContext.Interests.ToArrayAsync();

    public async Task<Interest?> GetInterestByName(string name) =>
        await _dbContext.Interests.FirstOrDefaultAsync(x => x.Name == name);
}