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
}