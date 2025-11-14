using Microsoft.AspNetCore.Mvc;

public class EventsPageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Create(string name, string description, DateTime start, DateTime end, )
}