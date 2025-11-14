using System.Security.Claims;
using HakatonProject.Models;
using HakatonProject.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly EventRepository _eventRepository;
    private readonly PlaceRepository _placeRepository;
    private readonly UserRepository _userRepository;

    private readonly CurrentUserService _currentUserService;

    public EventController(EventRepository eventRepository, UserRepository userRepository, CurrentUserService currentUserService, PlaceRepository placeRepository)
    {
        _eventRepository = eventRepository;
        _placeRepository = placeRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<Event>>> GetEventList()
    {
        var events = _eventRepository.GetEvents();
        return Ok(events);
    }

    [HttpGet]
    public async Task<ActionResult<Event>> GetEvent(long id)
    {
        var _event = await _eventRepository.TryGetEvent(id);

        return Ok(_event);
    }


    [HttpPost("create")]
    public async Task<ActionResult> CreateEvent(CreateEventDTO dto)
    {
        try{
            var userId = _currentUserService.GetCurrentUserId();
            if(userId == null)
                return Unauthorized("User not authorized");

            var user = await _userRepository.GetUser(userId.Value);
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
