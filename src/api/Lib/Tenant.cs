namespace api.Lib
{
    public class Tenant : ITenant
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Tenant(IHttpContextAccessor httpContextAccessor) => 
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
