using HakatonProject.Data;
using Microsoft.EntityFrameworkCore;

namespace HakatonProject.Models.Repositories;

public class PlaceRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;

    public async Task<Place[]> GetAllPlaces() => await dbContext.Places.ToArrayAsync();
    
    public async Task<Place?> GetPlace(long id) => await dbContext.Places.FirstOrDefaultAsync(p => p.Id == id);
    
    public async Task AddPlace(Place place)
    {
        await dbContext.Places.AddAsync(place);
        await dbContext.SaveChangesAsync();
    } 
}