using HakatonProject.Data;
using Microsoft.EntityFrameworkCore;


public class EventRepository(ApplicationDataDbContext dbContext)
{
    private readonly ApplicationDataDbContext _dbContext = dbContext;
    
    public async Task TryAddEvent(Event e)
    {
        await dbContext.Events.AddAsync(e);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Event?> TryGetEvent(long id) => await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
    
    public async Task<Event[]> GetEvents() => await dbContext.Events.ToArrayAsync();

    public async Task TryCreateEvent(Event _event)
    {
        await dbContext.Events.AddAsync(_event);
        await dbContext.SaveChangesAsync();
    }

    public async Task<EventTypeDTO[]> GetUserEvents(long ownerId)
    {
        var eventsList = await _dbContext.Events.Where(x => x.Owner.Id == ownerId).GroupBy(e => e.Type)
            .Select(g => new EventTypeDTO
            {
                Type = g.Key,
                Count = g.Count()
            }).ToArrayAsync();

        return eventsList;
    }
}
