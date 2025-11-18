using HakatonProject.Data;
using HakatonProject.Models;
using Microsoft.EntityFrameworkCore;


public class UserRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;

    public async Task<User?> GetUser(string username) => await dbContext.Users.FirstOrDefaultAsync(x => x.Login == username);

    public async Task<User?> GetUser(int id) => await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddUser(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public string GetUserNameById(long id)
    {
        var user = dbContext.Users.FirstOrDefault(x => x.Id == id);

        if(user == null)
            return "Гость";

        return user.Name;
    }
}