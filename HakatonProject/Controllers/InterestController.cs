using Microsoft.AspNetCore.Mvc;

namespace HakatonProject.Controllers;

public class InterestController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}