using DiamondAPI.Interfaces;
using DiamondAPI.Models;

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

        public async Task<bool> SendOrderConfirmRequest()
        {
            var orders = await _orderRepo.GetOrdersWithStatus("Processing");

            if (orders == null)
            {
                return false;
            }

            foreach (var order in orders)
            {
                Console.WriteLine(order.OrderStatus);
                if (order.OrderDate.HasValue && order.OrderDate.Value.AddHours(24) <= DateTime.Now && !string.IsNullOrWhiteSpace(order.OrderEmail))
                {
                    await _emailService.SendEmailAsync(order.OrderEmail, "Order confirm request", $"Your order with ID {order.OrderId} has been received, please press this link to confirm your order: .");
                    await _orderRepo.UpdateOrderStatusConfirmationSent(order);
                }
            }
            return true;
        }
    }
}
