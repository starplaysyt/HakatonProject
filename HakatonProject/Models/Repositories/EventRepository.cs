using HakatonProject.Data;
using HakatonProject.Data.Migrations;

public class EventRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;

    public async  Task<List<Event>> GetEvents() =>
        dbContext.Events.ToList();

    public async Task CreateEvent(Event _event)
    {
        await dbContext.AddAsync(_event);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<string>> GetOwnerEventTypes(long ownerId) => 
        dbContext.Events.Where(x => x.EventOwner.Id == ownerId).Select(x => x.Type).ToList();
}