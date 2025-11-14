using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HakatonProject.Models;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor, UserRepository userRepository)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserRepository _userRepository = userRepository;

    public long? GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("userId")?.Value;

        return long.TryParse(userIdClaim, out long userId) ? userId : null;
    }

    public string GetCurrentUserName()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("userId")?.Value;

        long.TryParse(userIdClaim, out long userId);

        string name = _userRepository.GetUserNameById(userId);

        return name;
    }
}