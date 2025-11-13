using HakatonProject.Data;
using HakatonProject.Models;
using Microsoft.EntityFrameworkCore;

public class ContactRepository
{
    private readonly ApplicationDataDbContext _dbContext;

    public ContactRepository(ApplicationDataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserContact?> GetUserContactAsyncById(long id) =>
        await _dbContext.UserContacts.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<UserContact[]> GetAllContacts() =>
        await _dbContext.UserContacts.ToArrayAsync();

    public async Task CreateUserContact(UserContact userContact)
    {
        await _dbContext.UserContacts.AddAsync(userContact);
        await _dbContext.SaveChangesAsync();
    }
}