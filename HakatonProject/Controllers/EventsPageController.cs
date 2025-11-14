using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
public class EventsPageController(EventRepository repo) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    [Route("create-event")]
    public async Task<IActionResult> CreateEvent(
        [FromForm] string nameEvent,
        [FromForm] string description,
        [FromForm] string startTime,
        [FromForm] string endTime,
        [FromForm] IFormFile eventImage)
    {
        // Проверка данных
        if (string.IsNullOrWhiteSpace(nameEvent))
            return BadRequest("Название мероприятия не указано");

        if (eventImage is { Length: > 0 })
        {
            // Пример: сохраняем файл на сервер
            var uploadsFolder = Path.Combine("wwwroot/uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, eventImage.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await eventImage.CopyToAsync(stream);
            }
        }

        // Можно создать объект Event и сохранить в БД
        var newEvent = new Event
        {
            Name = nameEvent,
            Description = description,
            TimeStart = DateTime.Parse(startTime),
            TimeEnd = DateTime.Parse(endTime),
            ImagePath = eventImage != null ? Path.Combine("uploads", eventImage.FileName) : null
        };
        
        await repo.TryAddEvent(newEvent);

        return RedirectToAction("Index", "Home");
    }
}