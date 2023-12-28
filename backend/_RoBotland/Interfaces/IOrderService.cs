using _RoBotland.Enums;
using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IOrderService
    {
        OrderDto PlaceOrderByLoggedInUser(ISession session, string username, OrderOptionsDto orderOptions);
        OrderDto PlaceOrderWithoutRegister(ISession session,UserDetailsDto userDetails);
        List<OrderDto> GetOrders();
        OrderDto ChangeOrderStatus(Guid id,OrderStatus orderStatus);

    }
}
