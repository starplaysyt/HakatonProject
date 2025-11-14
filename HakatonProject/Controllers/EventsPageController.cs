using Microsoft.AspNetCore.Mvc;

public class EventsPageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}