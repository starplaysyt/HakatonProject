using System.Security.Claims;
using HakatonProject.Models;
using HakatonProject.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly EventRepository _eventRepository;
    private readonly PlaceRepository _placeRepository;
    private readonly UserRepository _userRepository;

    public EventController(EventRepository eventRepository, UserRepository userRepository, PlaceRepository placeRepository)
    {
        _eventRepository = eventRepository;
        _placeRepository = placeRepository;
        _userRepository = userRepository;
    }

    [HttpGet("list")]
    public ActionResult<List<Event>> GetEventList()
    {
        var events = _eventRepository.GetEvents();
        return Ok(events);
    }


    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateEvent(CreateEventDTO dto)
    {
        var place = await _placeRepository.GetPlace(dto.PlaceId) ?? throw new Exception("No place founded");
        if(place == null)
            return BadRequest("Place not founded");

        Event newEvent = new Event{
            Owner = await _userRepository.GetUser(User.FindFirst("userId").Value),
            Name = dto.Name,
            Description = dto.Description,
            Place = place,
            TimeStart = dto.TimeStart,
            TimeEnd = dto.TimeEnd,
            Type = dto.Type
        };

        try
        {
            await _eventRepository.TryCreateEvent(newEvent);
            return Ok(newEvent);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
