using DiamondAPI.Models;

namespace DiamondAPI.DTOs.Order
{
    public class OrderRequestDTO
    {
        public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
    }
}
