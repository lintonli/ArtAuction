using ORDERSERVICE.Models;
using ORDERSERVICE.Models.Dtos;

namespace ORDERSERVICE.Service.Iservice
{
    public interface IOrder
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(Guid Id);
        Task<string> AddOrder(Order order);
        Task<string> DeleteOrder(Order ord);
        Task<Order>GetOrderbyBidId(Guid bidId);
        Task<StripeRequestDto>MakePayments(StripeRequestDto stripeRequestDto, string token );
        Task<bool> ValidatePayments(Guid OrderId, string token);
        Task saveChanges();
    }
}
