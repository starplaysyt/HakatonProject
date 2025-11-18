using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


public class AuthPageController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}