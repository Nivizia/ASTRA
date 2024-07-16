using DiamondAPI.DTOs.Orderitem;

namespace DiamondAPI.DTOs.Order
{
    public class CreateOrderRequestDTO
    {
        public Guid? CustomerId { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? OrderFirstName { get; set; }

        public string? OrderLastName { get; set; }

        public string? OrderEmail { get; set; }

        public string? OrderPhoneNumber { get; set; }

        public virtual List<CreateOrderitemRequestDTO> Orderitems { get; set; } = new List<CreateOrderitemRequestDTO>();
    }
}