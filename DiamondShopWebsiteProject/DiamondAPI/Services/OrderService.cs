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
                if (order.OrderDate.HasValue && order.OrderDate.Value.AddHours(24) <= DateTime.Now && order.OrderEmail != null)
                {
                    await _emailService.SendEmailAsync(order.OrderEmail, "Order confirm request", $"Your order with ID {order.OrderId} has been received, please press this link to confirm your order: http://localhost:5173/confirmation-email?c=" + order.CustomerId + "&o=" + order.OrderId);
                    await _orderRepo.UpdateOrderStatusConfirmationSent(order);
                }
            }
            return true;
        }

        public async Task<bool> ChangeOrderPostponed()
        {
            var orders = await _orderRepo.GetOrdersWithStatus("ConfirmationSent");

            if (orders == null)
            {
                return false;
            }

            foreach (var order in orders)
            {
                if (order.OrderDate.HasValue && order.OrderDate.Value.AddHours(48) <= DateTime.Now && order.OrderEmail != null)
                {
                    await _orderRepo.UpdateOrderStatusPostponed(order);
                    await _emailService.SendEmailAsync(order.OrderEmail, "Order postponed", $"Your order with ID {order.OrderId} has been postponed please press confirm to continue with the order or cancel.");
                }
            }
            return true;
        }
    }
}
