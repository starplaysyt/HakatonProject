using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class EventsPageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}