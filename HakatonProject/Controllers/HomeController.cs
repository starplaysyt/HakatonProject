using System.Diagnostics;
using System.Security.Claims;
using HakatonProject.Data;
using HakatonProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HakatonProject.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDataDbContext _context;

    private readonly ILogger _logger;

    public HomeController(ILogger logger, ApplicationDataDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {

            var username = User.Identity.Name;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst("userId")?.Value;

            Console.WriteLine(username + role + userId);
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Login()
    {
        
        return View();
    }
    
    public async Task<IActionResult> Register()
    {
        ViewBag.Faculties = await _context.Faculties.ToListAsync();
        return View();
    }
}