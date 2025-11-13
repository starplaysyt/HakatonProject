using HakatonProject.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserRepository _userRepository) : Controller
{
    private readonly UserRepository userRepository = _userRepository;

    [HttpGet("{username}")]
    public ActionResult<User> GetUserByUserName(string username)
    {
        try
        {
            var user = userRepository.GetUserByUserName(username);
            return Ok(user);
        }
        catch(Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}