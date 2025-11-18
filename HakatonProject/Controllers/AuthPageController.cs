using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("login")]
public class AuthPageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}