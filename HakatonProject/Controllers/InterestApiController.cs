using Microsoft.AspNetCore.Mvc;

namespace HakatonProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterestApiController(InterestRepository interestRepo, EventRepository eventRepo, UserInterestRepository userInterestRepository) : Controller
{
    private readonly InterestRepository InterestRepo = interestRepo;
    
    private readonly EventRepository EventRepo = eventRepo;

    private readonly UserInterestRepository _userInterestRepository =userInterestRepository;

    [HttpGet]
    [Route("events-for-interest")]
    public async Task<IActionResult> GetEventsByInterest(long interestId)
    {
        return Ok(await EventRepo.GetEvents(interestId));
    }

    [HttpGet]
    [Route("get-interests")]
    public async Task<IActionResult> GetInterestsByUser(long userId)
    {
        return Ok(_userInterestRepository.GetInterestsByUserId(userId));
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetAllInterests()
    {
        var interests = await InterestRepo.GetAllInterests();

        return Ok(interests);
    }
}