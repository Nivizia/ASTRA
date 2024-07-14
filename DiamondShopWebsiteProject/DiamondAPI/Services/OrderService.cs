using DiamondAPI.Interfaces;

namespace DiamondAPI.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public void UpdateOrderStatus()
        {
            var orders = _orderRepo.GetOrdersWithStatus("Processing");

            if (orders == null)
            {
                return;
            }

            foreach (var order in orders)
            {
                if (order.OrderDate.HasValue && order.OrderDate.Value.AddHours(24) <= DateTime.Now)
                {
                    order.OrderStatus = "Confirmed";
                    _orderRepo.UpdateOrder(order);
                }
            }

            _orderRepo.Save();
        }
    }
}
