using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using HakatonProject.Controllers.DTOs;
using HakatonProject.Data;
using HakatonProject.Models;
using HakatonProject.Models.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class UserController(ApplicationDataDbContext context, UserRepository usrRepo, FacultiesRepository facRepo) : Controller
{
    private static byte[] _universalKey = "AKJS-189A-1293-KLZQJAHSDJHAJSHHHJZHXKCKHKZXHKCHK"u8.ToArray();
    
    private readonly UserRepository userRepository = usrRepo;
    
    private readonly FacultiesRepository facultyRepository = facRepo;
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromForm] string username,
        [FromForm] string password)
    {
        Console.WriteLine("LOGIN ENTERED");
        //trying login as simple user
        var user = await userRepository.GetUser(username);
        
        if (user is null) return Unauthorized();

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.UserGroup),
            new Claim("userId", user.Id.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, 
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(6)
        };


        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
        
        return RedirectToAction("Index", "Home");
        //return Json(new LoginResultDTO {UserId = user.Id.ToString(), UserName = user.Login, UserRole = user.UserGroup});
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromForm] string name,
        [FromForm] string username,
        [FromForm] string password)
    {
        //if (userGroup is not ("Owner" or "User")) return UnprocessableEntity("Invalid UserGroup");

        if (await userRepository.GetUser(username) is not null) return UnprocessableEntity("Username occupied");
        
        //var faculty = await facultyRepository.GetFacultyById(facultyId);
        
        //if (faculty is null) return UnprocessableEntity("Faculty Id is invalid");
        
        var user = new User()
        {
            Name = name,
            Login = username,
            Password = password,
            //Job = job,
            // UserFaculty = faculty,
            // UserGroup = userGroup
        };
        
        var res = userRepository.AddUser(user);

        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    
    [HttpGet("{username}")]
    [Authorize]
    public async Task<ActionResult> GetUserByUserName(string username)
    {
        return Ok();
    }
}