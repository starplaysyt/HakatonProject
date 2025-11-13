using System.Diagnostics;
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
        Console.WriteLine($"{_context.Database.IsSqlite()}");
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
        // если нужно показать список факультетов — подгружаем
        _context.Faculties.Add(new Faculty()
        {
            Name = "testfacultity",
            Description = "testfacultity description"
        });
        
        await _context.SaveChangesAsync();
        
        ViewBag.Faculties = await _context.Faculties.ToListAsync();
        return View();
    }
}