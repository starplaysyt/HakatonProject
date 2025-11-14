using Microsoft.AspNetCore.Mvc;

namespace HakatonProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterestApiController(InterestRepository interestRepo, EventRepository eventRepo) : Controller
{
    private readonly InterestRepository InterestRepo = interestRepo;
    
    private readonly EventRepository EventRepo = eventRepo;

    [HttpGet]
    [Route("events-for-interest")]
    public async Task<IActionResult> GetEventsByInterest(long interestId)
    {
        return Ok(await EventRepo.GetEvents(interestId));
    }
}