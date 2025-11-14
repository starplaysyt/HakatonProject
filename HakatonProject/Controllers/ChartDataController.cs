using System.Diagnostics.Eventing.Reader;
using System.Text.Json;
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
        var chartData = new
        {
            labels = new[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" },
            datasets = new[]
            {
                new
                {
                    label = "Блок 1",
                    data = new object[]
                    {
                        new { x = "ПН", y = new double[]{10,12}, info = "Лекция по искусственному интеллекту" },
                        new { x = "ВТ", y = new double[]{11,13}, info = "Семинар по квантовой физике" },
                        new { x = "СР", y = new double[]{10,11}, info = "Мастер-класс по робототехнике" },
                        new { x = "ЧТ", y = new double[]{12,14}, info = "Практикум по кибербезопасности" },
                        new { x = "ПТ", y = new double[]{10,12}, info = "Лекция по математическому моделированию" },
                        new { x = "СБ", y = (double[]?)null, info = "" },
                        new { x = "ВС", y = new double[]{11,12}, info = "Воркшоп по проектному управлению" }
                    },
                    backgroundColor = "#4FC3F7",
                    borderRadius = 4,
                    borderSkipped = false
                },
                new
                {
                    label = "Блок 2",
                    data = new object[]
                    {
                        new { x = "ПН", y = new double[]{13,15}, info = "Лабораторная работа по биоинформатике" },
                        new { x = "ВТ", y = new double[]{14,16}, info = "Конференция по нанотехнологиям" },
                        new { x = "СР", y = new double[]{12,14}, info = "Семинар по финансовым технологиям" },
                        new { x = "ЧТ", y = new double[]{15,16}, info = "Лекция по промышленному дизайну" },
                        new { x = "ПТ", y = new double[]{13,15}, info = "Мастер-класс по блокчейн-разработке" },
                        new { x = "СБ", y = (double[]?)null, info = "" },
                        new { x = "ВС", y = new double[]{14,15}, info = "Воркшоп по стратегическому менеджменту" }
                    },
                    backgroundColor = "#81C784",
                    borderRadius = 4,
                    borderSkipped = false
                },
                new
                {
                    label = "Блок 3",
                    data = new object[]
                    {
                        new { x = "ПН", y = new double[]{16,19.3}, info = "Хакатон по машинному обучению" },
                        new { x = "ВТ", y = new double[]{13,19.2}, info = "Проектная работа по разработке ПО" },
                        new { x = "СР", y = new double[]{18.7,19.8}, info = "Тренинг по лидерству" },
                        new { x = "ЧТ", y = new double[]{20,21}, info = "Вечерняя лекция по философии технологий" },
                        new { x = "ПТ", y = new double[]{19,22}, info = "Фестиваль научных стартапов" },
                        new { x = "СБ", y = (double[]?)null, info = "" },
                        new { x = "ВС", y = new double[]{14,15}, info = "Воркшоп по аналитике данных" }
                    },
                    backgroundColor = "#fffc63",
                    borderRadius = 4,
                    borderSkipped = false
                }
            }
        };

        return new JsonResult(chartData, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetOwnerEvents(long ownerId)
    {
        var events = await _eventRepository.GetUserEvents(ownerId);

        return Ok(events);
    }
}