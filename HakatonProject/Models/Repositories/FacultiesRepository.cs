using HakatonProject.Data;
using Microsoft.EntityFrameworkCore;

namespace HakatonProject.Models.Repositories;

public class FacultiesRepository(ApplicationDataDbContext _dbContext)
{
    private readonly ApplicationDataDbContext dbContext = _dbContext;

    public async Task<Faculty[]> GetAllFaculties() => 
        await dbContext.Faculties.ToArrayAsync();
    
    public async Task<Faculty?> GetFacultyByName(string name) => 
        await dbContext.Faculties.FirstOrDefaultAsync(n => n.Name == name);
    
    public async Task<Faculty?> GetFacultyById(long id) =>
        await dbContext.Faculties.FirstOrDefaultAsync(n => n.Id == id);

    public async Task AddFaculty(Faculty faculty)
    {
        await dbContext.Faculties.AddAsync(faculty);
        await dbContext.SaveChangesAsync();
    }
    
}