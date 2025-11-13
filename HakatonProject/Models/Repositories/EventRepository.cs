using HakatonProject.Data;


public enum EventRepositoryErrors
{
    None = 0,
    EventNotFound = 1,
}

public class EventRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;

    public EventRepositoryErrors TryAddEvent(Event e)
    {
        dbContext.Events.Add(e);
        dbContext.SaveChanges();
        return EventRepositoryErrors.None;
    }
}