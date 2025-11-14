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
    [Route("owner-events")]
    public IActionResult Get()
    {
        var data = new
        {
            labels = new[] {"ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС"},
            datasets = new[]
            {
                // {
                //     label: 'Morning',
                //     data: [3],    // высота блока
                //     backgroundColor: '#82D1B3',
                //     stack: 'week'
                // },
                // {
                //     label: 'Day',
                //     data: [5],
                //     backgroundColor: '#D9B884',
                //     stack: 'week'
                // },
                // {
                //     label: 'Evening',
                //     data: [2],
                //     backgroundColor: '#D19282',
                //     stack: 'week'
                // }
                new
                {
                    label = "Morning",
                    data = new[] {3},
                    backgroundColor = "#D9B84",
                    stack = "week"
                },
                // new
                // {
                //     label = "Активность",
                //
                //     // Массив массивов (float[][])
                //     data = new float?[][]
                //     {
                //         new float?[] { 18f, 19.5f },   // ПН
                //         new float?[] { 13f, 15f },     // ВТ
                //         new float?[] { 16.5f, 18.5f }, // СР
                //         new float?[] { 10f, 13f },     // ЧТ
                //         new float?[] { 14f, 17f },     // ПТ
                //         new float?[] { 10.5f, 15f },   // СБ
                //         null                           // ВС
                //     },
                //
                //     backgroundColor = new[]
                //     {
                //         "#82D1B3",
                //         "#D9B884",
                //         "#D19282",
                //         "#B38FDB",
                //         "#82D19A",
                //         "#D7D789",
                //         "transparent"
                //     },
                //
                //     borderRadius = 8,
                //     borderSkipped = false
                // }
            }
        };

        return Ok(data); // JSON для Chart.js
    }
    
    public async Task<IActionResult> GetOwnerEvents(long ownerId)
    {
        var events = await _eventRepository.GetUserEvents(ownerId);

        return Ok(events);
    }
}