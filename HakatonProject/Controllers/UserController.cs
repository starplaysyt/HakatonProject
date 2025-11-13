using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HakatonProject.Data;
using HakatonProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly UserRepository userRepository;

    public UserController(ApplicationDataDbContext context)
    {
        userRepository = new UserRepository(context);
    }

    [HttpGet("{username}")]
    public ActionResult<User> GetUserByUserName(string username)
    {
        try
        {
            var user = userRepository.GetUserByUserName(username);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("/login")]
    public async Task<ActionResult> Login(string username, string password)
    {
        //trying login as simple user
        var user = await userRepository.TryGetFullUser(username, password);

        //if there is no such user - trying to log in as owner, so owners can also log in via standard form.
        //yes, I know that this architecture is fucked.
        if (user == null)
        {
            // var owner = await userRepository.TryGetFullOwner(username, password);
            // if (owner is null) return Unauthorized();
            //
            // var ownerClaims = new[]
            // {
            //     new Claim(ClaimTypes.Name, owner.Name),
            //     new Claim(ClaimTypes.Role, "Owner"),
            //     new Claim("ownerId", owner.Id.ToString())
            // };
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim("userId", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_key_123!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(tokenString);
    }
}