using System;
using System.Collections.Generic;

namespace api.Models
{
    public partial class Tenant
    {
        public Guid TenantId { get; set; }
        public string TenantAadtentantid { get; set; } = null!;
        public string TenantName { get; set; } = null!;
        public string TenantDbserver { get; set; } = null!;
        public string TenantDbname { get; set; } = null!;
    }
}
