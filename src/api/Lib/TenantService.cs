namespace api.Lib
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantService(IHttpContextAccessor httpContextAccessor) => 
            _httpContextAccessor = httpContextAccessor;

        public string GetTenantId()
        {
            string tenantId = "";
            var tenantIdClaims = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/identity/claims/tenantid");
            if (tenantIdClaims.Any())
            {
                tenantId = tenantIdClaims.FirstOrDefault().Value;
            }
            return tenantId;
        }
    }
}
