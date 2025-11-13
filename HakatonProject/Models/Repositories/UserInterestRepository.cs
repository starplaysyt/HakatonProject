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

    public async Task<Interest?> GetInterestByUser(User user)
    {
        UserInterest? userInterest = await _dbContext.UserInterests.FirstOrDefaultAsync(x => x.User == user);

        return userInterest.Interest ?? throw new Exception($"No interest with user: {user}");
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