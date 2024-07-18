using DiamondAPI.DTOs.Order;
using DiamondAPI.DTOs.RingPairing;
using DiamondAPI.Interfaces;
using DiamondAPI.Mappers;
using DiamondAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiamondAPI.Controllers
{
    [Route("DiamondAPI/Models/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IDiamondRepository _diamondRepo;
        private readonly IRingRepository _ringRepo;
        private readonly IRingPairingRepository _ringPairingRepo;
        private readonly IPendantRepository _pendantRepo;
        private readonly IPendantPairingRepository _pendantPairingRepo;
        private readonly IEarringRepository _earringRepo;
        private readonly IEarringPairingRepository _earringPairingRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderitemRepository _orderItemRepo;

        private readonly EmailService _emailService;

        public OrdersController(ICustomerRepository customerRepos, IRingPairingRepository ringPairingRepo, IPendantPairingRepository pendantPairingRepo, IOrderRepository orderRepo, IOrderitemRepository orderItemRepo, IRingRepository ringRepo, IPendantRepository pendantRepo, IDiamondRepository diamondRepo, IEarringRepository earringRepo, IEarringPairingRepository earringPairingRepo, EmailService emailService)
        {
            _customerRepo = customerRepos;
            _diamondRepo = diamondRepo;
            _ringRepo = ringRepo;
            _ringPairingRepo = ringPairingRepo;
            _pendantRepo = pendantRepo;
            _pendantPairingRepo = pendantPairingRepo;
            _earringRepo = earringRepo;
            _earringPairingRepo = earringPairingRepo;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderRequestDTO createOrderRequestDTO)
        {
            var Customer = await _customerRepo.GetByIDAsync(createOrderRequestDTO.CustomerId);
            if (Customer == null)
                return NotFound("User could not be found.");

            if (createOrderRequestDTO.Orderitems == null || createOrderRequestDTO.Orderitems.Count == 0)
                return BadRequest("Order items are required and cannot be empty.");

            var Order = createOrderRequestDTO.ToOrderFromCreateDTO();

            List<Orderitem> orderItems = new List<Orderitem>();
            List<Ringpairing> ringPairings = new List<Ringpairing>();
            List<Pendantpairing> pendantPairings = new List<Pendantpairing>();
            List<Earringpairing> earringPairings = new List<Earringpairing>();

            foreach (var orderItem in createOrderRequestDTO.Orderitems)
            {
                var productType = orderItem.ProductType?.ToLower();

                if (productType == null)
                    return BadRequest("Product type is required.");

                if (productType == "ringpairing")
                {
                    var ring = await _ringRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.RingId);
                    if (ring == null)
                        return NotFound("The specified ring could not be found. ID = " + orderItem.CreateRingPairingDTO?.RingId);

                    var isAvailable = await _diamondRepo.IsAvailable(orderItem.ProductId);
                    if (!isAvailable) return BadRequest("The diamond is already ordered. ID = " + orderItem.ProductId);

                    if (ring.StockQuantity < 1)
                        return BadRequest("The ring is out of stock. ID = " + orderItem.CreateRingPairingDTO?.RingId);

                    ring.StockQuantity--;

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreateRingPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.CreateRingPairingDTO?.DiamondId);

                    diamond.Available = false;

                    var ringPairing = orderItem.CreateRingPairingDTO?.ToRingPairingFromCreateDTO();
                    if (ringPairing == null)
                        return BadRequest("There was a problem instanciating a ringPairing object.");

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.RingPairingId = ringPairing.RProductId;

                    ringPairings.Add(ringPairing);
                    orderItems.Add(modelOrderItem);
                }
                else if (productType == "pendantpairing")
                {
                    var pendant = await _pendantRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.PendantId);
                    if (pendant == null)
                        return NotFound("The specified pendant could not be found. ID = " + orderItem.CreatePendantPairingDTO?.PendantId);

                    var isAvailable = await _diamondRepo.IsAvailable(orderItem.ProductId);
                    if (!isAvailable) return BadRequest("The diamond is already ordered. ID = " + orderItem.ProductId);

                    if (pendant.StockQuantity < 1)
                        return BadRequest("The pendant is out of stock. ID = " + orderItem.CreatePendantPairingDTO?.PendantId);

                    pendant.StockQuantity--;

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreatePendantPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.CreatePendantPairingDTO?.DiamondId);

                    diamond.Available = false;

                    var pendantPairing = orderItem.CreatePendantPairingDTO?.ToPendantPairingFromCreateDTO();
                    if (pendantPairing == null)
                        return BadRequest("There was a problem instanciating a pendantPairing object.");

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.PendantPairingId = pendantPairing.PProductId;

                    pendantPairings.Add(pendantPairing);
                    orderItems.Add(modelOrderItem);
                }
                else if (productType == "earringpairing")
                {
                    var earring = await _earringRepo.GetByIDAsync(orderItem.CreateEarringPairingDTO?.EarringId);
                    if (earring == null)
                        return NotFound("The specified earring could not be found. ID = " + orderItem.CreateEarringPairingDTO?.EarringId);

                    var isAvailable = await _diamondRepo.IsAvailable(orderItem.ProductId);
                    if (!isAvailable) return BadRequest("The diamond is already ordered. ID = " + orderItem.ProductId);

                    if (earring.StockQuantity < 1)
                        return BadRequest("The earring is out of stock. ID = " + orderItem.CreateEarringPairingDTO?.EarringId);

                    earring.StockQuantity--;

                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.CreateEarringPairingDTO?.DiamondId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.CreateEarringPairingDTO?.DiamondId);

                    diamond.Available = false;

                    var earringPairing = orderItem.CreateEarringPairingDTO?.ToEarringPairingFromCreateDTO();
                    if (earringPairing == null)
                        return BadRequest("There was a problem instanciating a earringPairing object.");

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.EarringPairingId = earringPairing.EProductId;

                    earringPairings.Add(earringPairing);
                    orderItems.Add(modelOrderItem);
                }
                else if (productType == "diamond")
                {
                    var diamond = await _diamondRepo.GetByIDAsync(orderItem.ProductId);
                    if (diamond == null)
                        return NotFound("The specified diamond could not be found. ID = " + orderItem.ProductId);

                    var isAvailable = await _diamondRepo.IsAvailable(orderItem.ProductId);
                    if (!isAvailable) return BadRequest("The diamond is already ordered. ID = " + orderItem.ProductId);

                    diamond.Available = false;

                    var modelOrderItem = orderItem.ToOrderItemFromCreateDTO();
                    modelOrderItem.OrderId = Order.OrderId;
                    modelOrderItem.DiamondId = orderItem.ProductId;

                    orderItems.Add(modelOrderItem);
                }
                else
                {
                    return BadRequest("Product type not recognised.");
                }
            }
            await _orderRepo.CreateOrder(Order);
            await _ringPairingRepo.CreateRingPairings(ringPairings);
            await _pendantPairingRepo.CreatePendantPairings(pendantPairings);
            await _earringPairingRepo.CreateEarringPairings(earringPairings);
            await _orderItemRepo.CreateOrderItems(orderItems);
            return Ok(Order.ToOrderRequestDTO());
        }

        [HttpGet]
        [Route("OrderHistory/{CustomerID}")]
        public async Task<IActionResult> GetOrders([FromRoute] Guid CustomerID)
        {
            var orders = await _orderRepo.GetAllOrders(CustomerID);
            var ordersDTO = orders.Select(o => o.ToOrderRequestDTO()).ToList();
            return Ok(ordersDTO);
        }

        [HttpGet]
        [Route("SearchOrders/{OrderFirstName}/{OrderLastName}/{OrderEmail}/{OrderPhone}")]
        public async Task<IActionResult> SearchOrders([FromRoute] string OrderFirstName, string OrderLastName, string OrderEmail, string OrderPhone)
        {
            var orders = await _orderRepo.GetOrdersByCusInfos(OrderFirstName, OrderLastName, OrderEmail, OrderPhone);
            var ordersDTO = orders.Select(o => o.ToOrderRequestDTO()).ToList();
            return Ok(ordersDTO);
        }

        [HttpGet]
        [Route("GetOrder/{OrderID}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid OrderID)
        {
            var order = await _orderRepo.GetOrderById(OrderID);
            if (order == null)
                return NotFound("Order not found.");
            return Ok(order.ToOrderRequestDTO());
        }

        [HttpPut]
        [Route("Cancel/{OrderID}")]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid OrderID)
        {
            var orderitems = await _orderItemRepo.GetOrderitemsByOrderId(OrderID);
            foreach (var orderitem in orderitems)
            {
                var OrderType = orderitem.ProductType?.ToLower();
                if (OrderType == "ringpairing")
                {
                    var ringpairing = await _ringPairingRepo.GetByIdAsync(orderitem.RingPairingId);

                    if (ringpairing == null)
                        return NotFound("Ringpairing not found.");

                    var diamond = await _diamondRepo.GetByIDAsync(ringpairing.DiamondId);

                    if (diamond == null)
                        return NotFound("Diamond not found.");

                    diamond.Available = true;

                    var ring = await _ringRepo.GetByIDAsync(ringpairing.RingId);

                    if (ring == null)
                        return NotFound("Ring not found.");

                    ring.StockQuantity++;
                }
                else if (OrderType == "pendantpairing")
                {
                    var pendantpairing = await _pendantPairingRepo.GetByIdAsync(orderitem.PendantPairingId);

                    if (pendantpairing == null)
                        return NotFound("Pendantpairing not found.");

                    var diamond = await _diamondRepo.GetByIDAsync(pendantpairing.DiamondId);

                    if (diamond == null) return NotFound("Diamond not found.");
                    diamond.Available = true;

                    var pendant = await _pendantRepo.GetByIDAsync(pendantpairing.PendantId);
                    if (pendant == null) return NotFound("Pendant not found.");
                    pendant.StockQuantity++;
                }
                else if (OrderType == "earringpairing")
                {
                    var earringpairing = await _earringPairingRepo.GetByIDAsync(orderitem.EarringPairingId);

                    if (earringpairing == null)
                        return NotFound("Earringpairing not found.");

                    var diamond = await _diamondRepo.GetByIDAsync(earringpairing.DiamondId);
                    if (diamond == null) return NotFound("Diamond not found.");
                    diamond.Available = true;

                    var earring = await _earringRepo.GetByIDAsync(earringpairing.EarringId);
                    if (earring == null) return NotFound("Earring not found.");
                    earring.StockQuantity++;
                }
                else if (OrderType == "diamond")
                {
                    var diamond = await _diamondRepo.GetByIDAsync(orderitem.DiamondId);
                    if (diamond == null) return NotFound("Diamond not found.");
                    diamond.Available = true;
                }
                else
                {
                    return BadRequest("Product type not recognised.");
                }
            }
            await _orderRepo.DeleteOrder(OrderID);
            return Ok("Order deleted.");
        }

        [HttpPut]
        [Route("Confirm/{OrderID}")]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] Guid OrderID)
        {
            var order = await _orderRepo.GetOrderById(OrderID);
            if (order == null)
                return NotFound("Order not found.");

            await _orderRepo.UpdateOrderStatusConfirmed(order);

            if (order.OrderEmail != null)
                await _emailService.SendEmailAsync(order.OrderEmail, "Order confirmed", $"Your order with ID {order.OrderId} has been confirmed.");

            return Ok(order);
        }
    }
}