using System.Security.Claims;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Common.Models;
using Throw;

namespace GymManagement.Api.Services
{
    public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
    {
        public CurrentUser GetCurrentUser()
        {
            _httpContextAccessor.HttpContext.ThrowIfNull();

            var claim = _httpContextAccessor.HttpContext.User.Claims
                .First(claim => claim.Type == "id");

            return new CurrentUser(Guid.Parse(claim.Value));
        }
    }
}