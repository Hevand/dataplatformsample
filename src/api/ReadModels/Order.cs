namespace api.ReadModels
{
    public class Order
    {
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public decimal TotalDue { get; set; }
    }
}
