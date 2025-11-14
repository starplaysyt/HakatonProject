public class CurrentUserService(IHttpContextAccessor httpContextAccessor)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public int? GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("userId")?.Value;

        return int.TryParse(userIdClaim, out int userId) ? userId : null;
    }
}