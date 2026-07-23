using EthioClass.Application.Common.Interfaces;

namespace EthioClass.Api.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public int? UserId
    {
        get
        {
            var value = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            return int.TryParse(value, out var id) ? id : null;
        }
    }

    public int? SchoolId
    {
        get
        {
            var value = httpContextAccessor.HttpContext?.User?.FindFirst("SchoolId")?.Value;
            return int.TryParse(value, out var id) ? id : null;
        }
    }

    public string? Role =>
        httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
}