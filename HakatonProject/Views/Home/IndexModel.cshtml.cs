using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly CurrentUserService _currentUserService;

    public IndexModel(CurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public string Name { get; set; } = "User";

    public async Task OnGet()
    {
        var userName = _currentUserService.GetCurrentUserName();
        Name = userName;
    }
}