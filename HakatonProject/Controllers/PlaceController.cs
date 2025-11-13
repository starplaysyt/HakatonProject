using HakatonProject.Models.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

[ApiController]
[Route("api/[controller]")]
public class PlaceController : ControllerBase
{
    private readonly PlaceRepository _placeRepository;

    public PlaceController(PlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreatePlace(CreatePlaceDTO dto)
    {
        Place newPlace = new Place();
        newPlace.Name = dto.Name;
        newPlace.Address = dto.Address;
        newPlace.Description = dto.Description;

        await _placeRepository.AddPlace(newPlace);

        return Ok();
    }
}
