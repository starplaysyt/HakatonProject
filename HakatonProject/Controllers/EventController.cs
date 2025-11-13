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
    public async Task<ActionResult> CreateEvent(Event _event)
    {
        try
        {
            await _eventRepository.CreateEvent(_event);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}