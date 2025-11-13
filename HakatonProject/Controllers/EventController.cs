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
    public ActionResult CreateEvent()
    {
        try
        {
            _eventRepository.CreateEvent();
            return Ok();   
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}