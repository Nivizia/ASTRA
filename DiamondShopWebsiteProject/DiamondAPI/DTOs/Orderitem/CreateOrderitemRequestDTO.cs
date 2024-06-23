namespace DiamondAPI.DTOs.Orderitem
{
    public class CreateOrderitemRequestDTO
    {
        public Guid? OrderId { get; set; }

        public Guid? ProductId { get; set; }

        public decimal? Price { get; set; }

        public string? ProductType { get; set; }
    }
}
