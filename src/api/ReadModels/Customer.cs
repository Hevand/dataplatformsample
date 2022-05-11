namespace api.ReadModels
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;

        public string FriendlyName { get; set; } = null!;

        public string? EmailAddress { get; set; } = null!;

        public IEnumerable<CustomerAddress> BillingAddress {get;set;} = null!;
        public IEnumerable<CustomerAddress> ShippingAddress {get;set;} = null!;
    }

    public class CustomerAddress
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string StateProvence { get; set; } = null!;
        public string CountryRegion { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
    }
}
