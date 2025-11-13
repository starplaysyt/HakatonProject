using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using HakatonProject.Data;
using HakatonProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class UserController(ApplicationDataDbContext context) : Controller
{
    private static byte[] _universalKey = "AKJS-189A-1293-KLZQ"u8.ToArray();
    
    private readonly UserRepository userRepository = new UserRepository(context);
    
    [HttpPost("/login")]
    public ActionResult Login(string username, string password)
    {
        //trying login as simple user
        var res = userRepository.TryGetUser(out var user, username);
        
        if (res is UserRepositoryErrors.UserNotFound || user is null) return Unauthorized();

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim("userId", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(_universalKey);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(tokenString);
    }

    [HttpGet("/register")]
    public ActionResult Register(byte[] jsonEntry)
    {
        var user = JsonSerializer.Deserialize<User>(jsonEntry);
        
        if (user is null) return BadRequest("WasNull");
        
        var res = userRepository.TryAddUser(user);

        if (res == UserRepositoryErrors.None)
            return Ok();

        return BadRequest(res.ToString());
    }
    
    [HttpGet("{username}")]
    [Authorize]
    public async Task<ActionResult> GetUserByUserName(string username)
    {
        return Ok();
    }
}