using HakatonProject.Data;
using Microsoft.EntityFrameworkCore;


public class EventRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;
    
    public async Task TryAddEvent(Event e)
    {
        await dbContext.Events.AddAsync(e);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Event?> TryGetEvent(long id) => await dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
    
    public async Task<Event[]> GetEvents() => await dbContext.Events.ToArrayAsync();
}