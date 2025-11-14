using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HakatonProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChartDataController : Controller
{
    private readonly EventRepository _eventRepository;

    public ChartDataController(EventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /*[HttpGet]
    public IActionResult Index()
    {
        return View();
    }*/

    [HttpGet]
    public IActionResult Get()
    {
        var data = new
        {
            labels = new[] { "Январь", "Февраль", "Март" },
            datasets = new[]
            {
                new
                {
                    label = "Продажи 2024",
                    data = new[] { 10, 20, 30 },
                    backgroundColor = "rgba(75, 192, 192, 0.5)"
                },
                new
                {
                    label = "Продажи 2025",
                    data = new[] { 15, 25, 35 },
                    backgroundColor = "rgba(255, 99, 132, 0.5)"
                },
                new
                {
                    label = "Продажи 2026",
                    data = new[] { 20, 15, 25 },
                    backgroundColor = "rgba(255, 206, 86, 0.5)"
                }
            }
        };

        return Ok(data); // JSON для Chart.js
    }

    [HttpGet]
    [Route("owner-events")]
    public async Task<IActionResult> GetOwnerEvents(long ownerId)
    {
        var events = await _eventRepository.GetUserEvents(ownerId);

        return Ok(events);
    }
}