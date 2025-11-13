using HakatonProject.Data;

namespace HakatonProject.Models.Repositories;

public enum UserEventRepositoryErrors
{
    None = 0,
    UserNotFound = 1,
    EventNotFound = 2
}

public class UserEventRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;
    
    public UserEventRepositoryErrors TryGetUserEvents(out Event[] events, User user)
    {
        events = dbContext.UserEvents.Where(ue => ue.User.Id == user.Id).Select(ue => ue.Event).ToArray();
        return UserEventRepositoryErrors.None;
    }
    
    private UserEventRepositoryErrors TryGetEventUsers(out User[] users, Event e)
    {
        users = dbContext.UserEvents.Where(ue => ue.Event.Id == e.Id).Select(ue => ue.User).ToArray();
        return UserEventRepositoryErrors.None;
    }
}