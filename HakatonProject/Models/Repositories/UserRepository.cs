using HakatonProject.Data;
using HakatonProject.Models;
using Microsoft.EntityFrameworkCore;

public class UserRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;

    public async Task<User> GetUserByUserName(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Имя пользователя не может быть пустым!");

        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Name == username);

        return user ?? throw new InvalidOperationException($"Пользователь с именем \"{user}\" не найден");
    }

    public async Task<User?> TryGetFullUser(string username, string password)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Login == username && x.Password == password);
    }

    // public async Task<Owner?> TryGetFullOwner(string username, string password)
    // {
    //     return await dbContext.Owners.FirstOrDefaultAsync(x => x.Login == username && x.Password == password);
    // }
}