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
                if (order.OrderDate.HasValue && order.OrderDate.Value.AddHours(24) <= DateTime.Now && !string.IsNullOrWhiteSpace(order.OrderEmail))
                {
                    order.OrderStatus = "Confirmed";
                    _orderRepo.UpdateOrder(order);

                    // Ensure order.OrderEmail is not null or whitespace before sending the email
                    _emailService.SendEmailAsync(order.OrderEmail, "Order Confirmed", $"Your order with ID {order.OrderId} has been confirmed.").Wait();
                }
            }

            _orderRepo.Save();
        }
    }
}
