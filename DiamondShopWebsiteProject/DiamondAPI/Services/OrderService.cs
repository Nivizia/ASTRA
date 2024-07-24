using DiamondAPI.Interfaces;
using DiamondAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiamondAPI.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderitemRepository _orderItemRepo;
        private readonly IDiamondRepository _diamondRepo;
        private readonly IRingRepository _ringRepo;
        private readonly IPendantRepository _pendantRepo;
        private readonly IRingPairingRepository _ringPairingRepo;
        private readonly IPendantPairingRepository _pendantPairingRepo;
        private readonly IEarringPairingRepository _earringPairingRepo;
        private readonly IEarringRepository _earringRepo;
        private readonly EmailService _emailService;
        private string _url;

        public OrderService(IOrderRepository orderRepo, IOrderitemRepository orderItemRepo, IDiamondRepository diamondRepo, IRingRepository ringRepo, IPendantRepository pendantRepo, IRingPairingRepository ringPairingRepo, IPendantPairingRepository pendantPairingRepo, EmailService emailService, IEarringRepository earringRepo, IEarringPairingRepository earringPairingRepo)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _diamondRepo = diamondRepo;
            _ringRepo = ringRepo;
            _pendantRepo = pendantRepo;
            _ringPairingRepo = ringPairingRepo;
            _pendantPairingRepo = pendantPairingRepo;
            _earringPairingRepo = earringPairingRepo;
            _earringRepo = earringRepo;
            _emailService = emailService;
            _url = "http://astradiamonds.com:5173";
        }

        /*
        private string GetRingName(Ring ring)
        {
            string RingName = "";

            // Helper function to return non-null values or an empty string
            static string safeValue(string value) => !string.IsNullOrEmpty(value) ? value : "";

            // Build the ring name based on the type
            if (ring.RingType != null && ring.RingType.TypeName.Equals("Solitaire", StringComparison.OrdinalIgnoreCase))
            {
                RingName = $"{safeValue(ring.RingSubtype.SubtypeName)} {safeValue(ring.FrameType.FrameTypeName)} {safeValue(ring.RingType.TypeName)} Engagement Ring in {safeValue(ring.MetalType.MetalTypeName)}".Trim();
            }
            else if (ring.RingType != null && ring.RingType.TypeName.Equals("Halo", StringComparison.OrdinalIgnoreCase))
            {
                RingName = $"{safeValue(ring.RingSubtype.SubtypeName)} {safeValue(ring.RingType.TypeName)} Diamond Engagement Ring in {safeValue(ring.MetalType.MetalTypeName)}".Trim();
            }
            else if (ring.RingType != null && ring.RingType.TypeName.Equals("Sapphire Sidestone", StringComparison.OrdinalIgnoreCase))
            {
                RingName = $"{safeValue(ring.RingSubtype.SubtypeName)} Sapphire and Diamond Engagement Ring in {safeValue(ring.MetalType.MetalTypeName)}".Trim();
            }
            else if (ring.RingType != null && ring.RingType.TypeName.Equals("Three Stone", StringComparison.OrdinalIgnoreCase))
            {
                RingName = $"{safeValue(ring.RingSubtype.SubtypeName)} {safeValue(ring.RingType.TypeName)} Diamond Engagement Ring in {safeValue(ring.MetalType.MetalTypeName)}".Trim();
            }

            // Add optional attributes like stoneCut
            if (ring.StoneCut != null)
            {
                RingName = $"{safeValue(ring.StoneCut.StoneCutName)} {RingName}".Trim();
            }

            // Remove any extra spaces
            RingName = Regex.Replace(RingName, @"\s+", " ").Trim();

            return RingName;
        }
        */

        // Placeholder for similar methods for Pendants and other jewelry types
        private string GetPendantName(Pendant pendant)
        {
            return pendant.Name ?? "Pendant";
        }

        public async Task<bool> UpdateStatusToProcessing()
        {
            var orders = await _orderRepo.GetOrdersWithStatus("Payment Received");

            if (orders == null)
            {
                return false;
            }

            foreach (var order in orders)
            {
                if (order.OrderDate.HasValue && order.OrderDate.Value.AddMinutes(5) <= DateTime.Now)
                {
                    await _orderRepo.UpdateOrderStatus(order.OrderId, "Processing");
                }
            }
            return true;
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
                    List<Orderitem> orderItems = await _orderItemRepo.GetOrderitemsByOrderId(order.OrderId);
                    string confirmToken = TokenHelper.GenerateToken(order.OrderId);
                    string cancelToken = TokenHelper.GenerateToken(order.OrderId);

                    string confirmUrl = $"{_url}/confirm-email?t={confirmToken}";
                    string cancelUrl = $"{_url}/cancel-email?t={cancelToken}";

                    // Construct the list of order items
                    string orderItemsHtml = "<ul>";
                    foreach (var item in orderItems)
                    {
                        Console.WriteLine(item.ProductType);
                        if (item.ProductType == "Diamond")
                        {
                            var diamondDetails = await _diamondRepo.GetByIDAsync(item.DiamondId);
                            if (diamondDetails == null)
                            {
                                throw new Exception("Diamond not found");
                            }

                            string diamondLink = $"{_url}/diamond/{item.DiamondId}?view=true";
                            string diamondDescription = $"{diamondDetails.CaratWeight} Carat {diamondDetails.Color}-{diamondDetails.Clarity} {diamondDetails.Cut} Cut {diamondDetails.Shape} Diamond (${item.Price})";
                            orderItemsHtml += $"<li>Diamond: <a href='{diamondLink}'>{diamondDescription}</a></li>";
                        }
                        else if (item.ProductType == "PendantPairing")
                        {
                            var pendantPairing = await _pendantPairingRepo.GetByIdAsync(item.PendantPairingId);
                            if (pendantPairing == null)
                            {
                                throw new Exception("Pendant pairing not found");
                            }

                            var pendantDetails = await _pendantRepo.GetByIDAsync(pendantPairing.PendantId);
                            var diamondDetails = await _diamondRepo.GetByIDAsync(pendantPairing.DiamondId);
                            if (pendantDetails == null || diamondDetails == null)
                            {
                                throw new Exception("Pendant or diamond not found");
                            }

                            string pendantLink = $"{_url}/pendant/{pendantDetails.PendantId}";
                            string diamondLink = $"{_url}/diamond/{diamondDetails.DProductId}?view=true";
                            string pendantDescription = GetPendantName(pendantDetails);
                            string diamondDescription = $"{diamondDetails.CaratWeight} Carat {diamondDetails.Color}-{diamondDetails.Clarity} {diamondDetails.Cut} Cut {diamondDetails.Shape} Diamond (${(item.Price - pendantDetails.Price):0.00})";
                            orderItemsHtml += $"<li>Pendant Jewelry:<ul><li>Diamond: <a href='{diamondLink}'>{diamondDescription}</a></li><li>Pendant: <a href='{pendantLink}'>{pendantDescription} (${pendantDetails.Price:0.00})</a></li></ul></li>";
                        }
                        else if (item.ProductType == "RingPairing")
                        {
                            var ringPairing = await _ringPairingRepo.GetByIdAsync(item.RingPairingId);
                            if (ringPairing == null)
                            {
                                throw new Exception("Ring pairing not found");
                            }

                            var ringDetails = await _ringRepo.GetByIDAsync(ringPairing.RingId);
                            var diamondDetails = await _diamondRepo.GetByIDAsync(ringPairing.DiamondId);
                            if (ringDetails == null || diamondDetails == null)
                            {
                                throw new Exception("Ring or diamond not found");
                            }

                            string ringLink = $"{_url}/ring/{ringDetails.RingId}";
                            string diamondLink = $"{_url}/diamond/{diamondDetails.DProductId}?view=true";

                            if (ringDetails.RingName == null)
                            {
                                throw new Exception("Ring not found");
                            }

                            string ringDescription = ringDetails.RingName;
                            string diamondDescription = $"{diamondDetails.CaratWeight} Carat {diamondDetails.Color}-{diamondDetails.Clarity} {diamondDetails.Cut} Cut {diamondDetails.Shape} Diamond (${(item.Price - ringDetails.Price):0.00})";
                            orderItemsHtml += $"<li>Ring Jewelry:<ul><li>Diamond: <a href='{diamondLink}'>{diamondDescription}</a></li><li>Ring: <a href='{ringLink}'>{ringDescription} (${ringDetails.Price:0.00})</a></li></ul></li>";
                        }
                    }
                    orderItemsHtml += "</ul>";

                    // Construct the email body
                    string emailBody = $@"
                    <p>Your order with ID: {order.OrderId} has been received.</p>
                    <p>List of items:</p>
                    {orderItemsHtml}
                    <p>Total: ${orderItems.Sum(o => o.Price):0.00}</p>
                    <p>Please choose one of the options below:</p>
                    <a href='{confirmUrl}' style='display:inline-block;padding:10px 20px;margin:10px;color:white;background-color:green;text-align:center;text-decoration:none;border-radius:5px;'>Confirm</a>
                    <a href='{cancelUrl}' style='display:inline-block;padding:10px 20px;margin:10px;color:white;background-color:red;text-align:center;text-decoration:none;border-radius:5px;'>Cancel</a>";

                    await _emailService.SendEmailAsync(order.OrderEmail, "ORDER CONFIRMATION REQUEST", emailBody);
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
                    await _emailService.SendEmailAsync(order.OrderEmail, "Order postponed", $"Your order with ID {order.OrderId} has been postponed. Please press confirm to continue with the order or cancel.");
                }
            }
            return true;
        }

        public async Task<bool> CheckExpiredDepositPendingOrders()
        {
            var orders = await _orderRepo.GetDepositPendingOrders();
            foreach (var order in orders)
            {
                if (order.OrderDate.HasValue && order.OrderDate.Value.AddHours(1) <= DateTime.Now && order.OrderEmail != null)
                {
                    await RevertOrder(order.OrderId);
                    await _emailService.SendEmailAsync(order.OrderEmail, "Order expired", $"Your order with ID {order.OrderId} has been cancelled due to overdue deposit payment.");
                }
            }
            return true;
        }

        public async Task<bool> RevertOrder(Guid OrderID)
        {
            var orderitems = await _orderItemRepo.GetOrderitemsByOrderId(OrderID);
            foreach (var orderitem in orderitems)
            {
                var OrderType = orderitem.ProductType?.ToLower();
                if (OrderType == "ringpairing")
                {
                    var ringpairing = await _ringPairingRepo.GetByIdAsync(orderitem.RingPairingId);

                    if (ringpairing == null)
                        throw new Exception("Ring pairing not found");

                    var diamond = await _diamondRepo.GetByIDAsync(ringpairing.DiamondId);

                    if (diamond == null)
                        throw new Exception("Diamond not found");

                    diamond.Available = true;

                    var ring = await _ringRepo.GetByIDAsync(ringpairing.RingId);

                    if (ring == null)
                        throw new Exception("Ring not found");

                    ring.StockQuantity++;
                }
                else if (OrderType == "pendantpairing")
                {
                    var pendantpairing = await _pendantPairingRepo.GetByIdAsync(orderitem.PendantPairingId);

                    if (pendantpairing == null)
                        throw new Exception("Pendant pairing not found");

                    var diamond = await _diamondRepo.GetByIDAsync(pendantpairing.DiamondId);

                    if (diamond == null)
                        throw new Exception("Diamond not found");

                    diamond.Available = true;

                    var pendant = await _pendantRepo.GetByIDAsync(pendantpairing.PendantId);
                    if (pendant == null)
                        throw new Exception("Pendant not found");

                    pendant.StockQuantity++;
                }
                else if (OrderType == "earringpairing")
                {
                    var earringpairing = await _earringPairingRepo.GetByIDAsync(orderitem.EarringPairingId);

                    if (earringpairing == null)
                        throw new Exception("Earring pairing not found");

                    var diamond = await _diamondRepo.GetByIDAsync(earringpairing.DiamondId);
                    if (diamond == null)
                        throw new Exception("Diamond not found");

                    diamond.Available = true;

                    var earring = await _earringRepo.GetByIDAsync(earringpairing.EarringId);
                    if (earring == null)
                        throw new Exception("Earring not found");

                    earring.StockQuantity++;
                }
                else if (OrderType == "diamond")
                {
                    var diamond = await _diamondRepo.GetByIDAsync(orderitem.DiamondId);
                    if (diamond == null)
                        throw new Exception("Diamond not found");

                    diamond.Available = true;
                }
                else
                {
                    throw new Exception("Invalid order type");
                }
            }
            await _orderRepo.ExpiredDepositOrder(OrderID);
            return true;
        }
    }
}

