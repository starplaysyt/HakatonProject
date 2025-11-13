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


    [HttpPost("create")]
    public async Task<ActionResult> CreateEvent(CreateEventDTO dto)
    {
        try{
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User not authenticated");

            if (!int.TryParse(userIdClaim, out int userId))
                return BadRequest("Invalid user ID format");

            var user = await _userRepository.GetUser(userId);
            if (user == null)
                return BadRequest("User not found in database");

            var place = await _placeRepository.GetPlace(dto.PlaceId);
            if (place == null)
                return BadRequest("Place not found");

            Event newEvent = new Event
            {
                Owner = user,
                Name = dto.Name,
                Description = dto.Description,
                Place = place,
                TimeStart = dto.TimeStart,
                TimeEnd = dto.TimeEnd,
                Type = dto.Type
            };

            await _eventRepository.TryCreateEvent(newEvent);
            return Ok(new { 
                message = "Event created successfully", 
                eventId = newEvent.Id 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
