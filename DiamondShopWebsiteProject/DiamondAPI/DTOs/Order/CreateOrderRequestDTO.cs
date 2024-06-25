using DiamondAPI.DTOs.Orderitem;

namespace DiamondAPI.DTOs.Order
{
    public class CreateOrderRequestDTO
    {
        public Guid? CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? OrderStatus { get; set; }

        public virtual List<OrderitemDTO> Orderitems { get; set; } = new List<OrderitemDTO>();
    }
}
