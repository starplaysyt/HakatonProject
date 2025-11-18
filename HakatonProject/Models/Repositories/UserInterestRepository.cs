using HakatonProject.Data;
using HakatonProject.Models;
using Microsoft.EntityFrameworkCore;

public class UserInterestRepository
{
    private readonly ApplicationDataDbContext _dbContext;

    private UserInterestRepository(ApplicationDataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Interest[]> GetInterestsByUserId(long id)
    {
        var result = 
            await _dbContext
                .UserInterests
                .Where(it => it.User.Id == id)
                .Select(ui => ui.Interest)
                .ToArrayAsync();

        return result ?? throw new Exception($"No interest with user: {id}");
    } 

    public async Task<User?> GetUserByInterest(Interest interest)
    {
        UserInterest? userInterest = await _dbContext.UserInterests.FirstOrDefaultAsync(x => x.Interest == interest);

        return userInterest.User ?? throw new Exception($"No user with interest: {interest}");
    }

    public async Task CreateUserInterest(UserInterest userInterest)
    {
        await _dbContext.UserInterests.AddAsync(userInterest);
        await _dbContext.SaveChangesAsync();
    }
}