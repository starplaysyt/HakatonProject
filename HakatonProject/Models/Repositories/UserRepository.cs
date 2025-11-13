using HakatonProject.Data;
using HakatonProject.Models;
using Microsoft.EntityFrameworkCore;

public enum UserRepositoryErrors
{
    None = 0,
    UserNotFound = 1,
    LoginOccupiedError = 2,
}

public class UserRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;

    public UserRepositoryErrors TryGetUser(out User? user, string? username)
    {
        user = dbContext.Users.FirstOrDefault(x => x.Login == username);
        
        return user is null ? UserRepositoryErrors.UserNotFound : UserRepositoryErrors.None;
    }

    public UserRepositoryErrors TryGetUser(out User? user, int id)
    {
        user = dbContext.Users.FirstOrDefault(x => x.Id == id);
        
        return user is null ? UserRepositoryErrors.UserNotFound : UserRepositoryErrors.None;
    }

    public UserRepositoryErrors TryAddUser(User user)
    {
        if (!dbContext.Users.Any(x => x.Login == user.Login))
            dbContext.Users.Add(user);
        else
            return UserRepositoryErrors.LoginOccupiedError;
        
        return UserRepositoryErrors.None;
    }

    public UserRepositoryErrors TryUpdateUser(User user)
    {
        if (dbContext.Users.Any(u => user.Id == u.Id))
            return UserRepositoryErrors.UserNotFound;
        
        dbContext.Users.Update(user);
        dbContext.SaveChanges();
        return UserRepositoryErrors.None;
    }
}