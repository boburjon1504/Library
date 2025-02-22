using Library.API.Services.Interfaces;

namespace Library.API.Services;

public class RequestUserContext(IHttpContextAccessor httpContextAccessor) : IRequestUserContext
{
    public Guid GetRequestUserId()
    {
        var id = httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type.Equals("UserId"))!.Value;

        return id is null ? Guid.Empty : Guid.Parse(id);
    }
}
