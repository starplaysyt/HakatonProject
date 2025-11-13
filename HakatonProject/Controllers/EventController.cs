using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly EventRepository _eventRepository;

    public EventController(EventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    [HttpGet("list")]
    public ActionResult<List<Event>> GetEventList()
    {
        var events = _eventRepository.GetEvents();
        return Ok(events);
    }


    [HttpPost]
    public async Task<ActionResult> CreateEvent(CreateEventDTO dto)
    {
        Event newEvent = new Event();

        newEvent.Name = dto.Name;
        newEvent.Description = dto.Description;
        newEvent.TimeStart = dto.TimeStart;
        newEvent.TimeEnd = dto.TimeEnd;

        try
        {
            await _eventRepository.TryCreateEvent(newEvent);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class CreateEventDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
}