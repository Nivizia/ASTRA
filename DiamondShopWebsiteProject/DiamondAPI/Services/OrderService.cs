using DiamondAPI.Interfaces;

namespace DiamondAPI.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly EmailService _emailService;

        public OrderService(IOrderRepository orderRepo, EmailService emailService)
        {
            _orderRepo = orderRepo;
            _emailService = emailService;
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

                    _emailService.SendEmailAsync(order.Orderemail, "Order Confirmed", $"Your order with ID {order.OrderId} has been confirmed.");
                }
            }

            _orderRepo.Save();
        }
    }
}
