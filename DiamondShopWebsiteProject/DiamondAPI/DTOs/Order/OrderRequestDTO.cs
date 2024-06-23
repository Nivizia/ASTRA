using DiamondAPI.DTOs.Orderitem;
using DiamondAPI.Models;

namespace DiamondAPI.DTOs.Order
{
    public class OrderRequestDTO
    {
        public virtual ICollection<OrderitemDTO> Orderitems { get; set; } = new List<OrderitemDTO>();
    }
}
