using AuctionMessageBus;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ORDERSERVICE.Data;
using ORDERSERVICE.Models;
using ORDERSERVICE.Models.Dtos;
using ORDERSERVICE.Service.Iservice;
using Stripe;
using Stripe.Checkout;

namespace ORDERSERVICE.Service
{
    public class OrderService : IOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IBid _bidService;
        private readonly IUser _userService;
        private readonly IMessageBus _messageBus;
        private readonly ResponseDto responseDto;

        public OrderService(ApplicationDbContext context, IBid bid, IUser user, IMessageBus messageBus, IConfiguration configuration)
        {
            _context = context;
            _bidService = bid;
            _userService = user;
            _messageBus= messageBus;
            _configuration = configuration;
            responseDto= new ResponseDto();
        }
        public async Task<string> AddOrder(Order order)
        {
          _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return "Order Added Successfully";
        }

        public async Task<string> DeleteOrder(Order ord)
        {
            _context.Orders.Remove(ord);
            await _context.SaveChangesAsync();
            return "Order deleted successfully";
        }

        public async Task<Order> GetOrderbyBidId(Guid bidId)
        {
            var bidorder = await _context.Orders.Where(x => x.BidId == bidId).FirstOrDefaultAsync();
            if (bidorder != null)
            {
                responseDto.Errormessage = "Bid id exists";

            }
            return bidorder;

        }

        public async Task<Order> GetOrderById(Guid Id)
        {
            return await _context.Orders.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<List<Order>> GetOrders()
        {
            var order = await _context.Orders.ToListAsync();
            return order;
        }

        public  async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {
            var order = await _context.Orders.Where(x => x.Id == stripeRequestDto.OrderId).FirstOrDefaultAsync();
            var bid = await _bidService.GetBidById(order.BidId);

            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };

            var item = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions()
                {
                    UnitAmount = (long)bid.BidPrice * 100,
                    Currency = "kes",
                    ProductData = new SessionLineItemPriceDataProductDataOptions()
                    {
                        Name = bid.Product.Name,
                        Description = bid.Product.Description,
                        Images = new List<string> { bid.Product.Image }
                    }
                },
                Quantity = 1
            };
            options.LineItems.Add(item);

            var service = new SessionService();
            Session session = service.Create(options);

            stripeRequestDto.StripeSessionUrl = session.Url;
            stripeRequestDto.StripeSessionId = session.Id;

            order.StripeSessionId = session.Id;
            order.Status = "Ongoing";
            await _context.SaveChangesAsync();
            return stripeRequestDto;
        }

        public async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidatePayments(Guid OrderId)
        {

            var order = await _context.Orders.Where(x => x.Id == OrderId).FirstOrDefaultAsync();

            var service = new SessionService();
            Session session = service.Get(order.StripeSessionId);

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                //the payment was success

                order.Status = "Paid";
                order.PaymentIntent = paymentIntent.Id;
                await _context.SaveChangesAsync();

                var user = await _userService.GetUserById(order.UserId);

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return false;
                }
                var bid = await _bidService.GetBidById(order.BidId);
                UserMessageDto userMessage = new UserMessageDto()
                {
                    Name=user.Name,
                    Email = user.Email,
                    ProductName =bid.Product.Name,
                    ProductImage = bid.Product.Image

                };
                string queueName = _configuration.GetValue<string>("QueuesAndTopics:OrderQueue");
                    await _messageBus.PublishMessage( userMessage , queueName);
                

                // Send an Email to User
    
                return true;

            }
            return false;
        }
    }
}
    

