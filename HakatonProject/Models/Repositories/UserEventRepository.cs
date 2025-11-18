using HakatonProject.Data;
using Microsoft.EntityFrameworkCore;

namespace HakatonProject.Models.Repositories;

public class UserEventRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;
    
    public async Task<Event[]> GetUserEvents(User user) =>
        await dbContext.UserEvents.Where(ue => ue.User.Id == user.Id).Select(ue => ue.Event).ToArrayAsync();

    
    private async Task<User[]> GetEventUsers(Event e) =>
        await dbContext.UserEvents.Where(ue => ue.Event.Id == e.Id).Select(ue => ue.User).ToArrayAsync();

    private async Task TryAddEvent(Event e, User user) => await dbContext.UserEvents.AddAsync(
        new UserEvent
        {
            Event = e, User = user
        }
    );


}