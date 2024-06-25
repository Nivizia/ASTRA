using DiamondAPI.DTOs.Orderitem;

namespace DiamondAPI.DTOs.Order
{
    public class CreateOrderRequestDTO
    {
        public Guid? CustomerId { get; set; }

        public decimal? TotalAmount { get; set; }

        public virtual List<CreateOrderitemRequestDTO> Orderitems { get; set; } = new List<CreateOrderitemRequestDTO>();
    }
}