

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class InterestController : Controller
{
    public IActionResult Index()
    {
        return View();
    } 
}