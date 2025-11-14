using Microsoft.AspNetCore.Mvc;

namespace HakatonProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterestController(UserInterestRepository repo) : Controller
{
    private readonly UserInterestRepository _repo = repo;

    [HttpGet]
    [Route("get-interests")]
    public async Task<IActionResult> GetInterestsByUser(long userId)
    {
        return Ok(_repo.GetInterestsByUserId(userId));
    }
    
    
}