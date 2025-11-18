using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AnalyticPageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}