namespace Conduit.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string? GetCurrentId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(type: "sud")?.Value;
        }
    }
}
