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

        public async Task<bool> UpdateOrderStatus()
        {
            var orders = await _orderRepo.GetOrdersWithStatus("Processing");

            if (orders == null)
            {
                Console.WriteLine("WAHHHHHH");
                return false;
            }
            Console.WriteLine(DateTime.Now);
            foreach (var order in orders)
            {
                Console.WriteLine(order.OrderStatus);
                if (order.OrderDate.HasValue && order.OrderDate.Value.AddMinutes(1) <= DateTime.Now && !string.IsNullOrWhiteSpace(order.OrderEmail))
                {
                    Console.WriteLine("WAHHHHHHHHH");
                    await _orderRepo.UpdateOrderStatus(order);

                    // Ensure order.OrderEmail is not null or whitespace before sending the email
                    await _emailService.SendEmailAsync(order.OrderEmail, "Order Confirmed", $"Your order with ID {order.OrderId} has been confirmed.");
                }
            }
            return true;
        }
    }
}
