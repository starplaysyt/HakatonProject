using HakatonProject.Data;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task<Event[]> TryGetUserEvents(User user)
    {
        var events = await dbContext.UserEvents.Where(ue => ue.User.Id == user.Id).Select(ue => ue.Event).ToArrayAsync();
        return events;
    }
    
    private async Task<User[]> TryGetEventUsers(Event e)
    {
        var users = await dbContext.UserEvents.Where(ue => ue.Event.Id == e.Id).Select(ue => ue.User).ToArrayAsync();
        return users;
    }

    private async Task TryAddEvent(Event e, User user) => await dbContext.UserEvents.AddAsync(
        new UserEvent
        {
            Event = e, User = user
        }
    );


}