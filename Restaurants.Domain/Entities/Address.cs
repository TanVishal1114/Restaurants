namespace Restaurants.Domain.Entities
{
    public class Address
    {
        public string? City { get; set; }
        public string Street { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
    }
}
