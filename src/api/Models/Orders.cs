namespace api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Amount { get; set; }
        public string? TenantId { get; set; }
    }
}
