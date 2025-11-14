using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class AnalyticPageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}