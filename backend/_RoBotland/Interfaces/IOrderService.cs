using _RoBotland.Enums;
using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IOrderService
    {
        OrderDto PlaceOrderByLoggedInUser(List<ShoppingCartItem> items, string username, OrderOptionsDto orderOptions);
        OrderDto PlaceOrderWithoutRegister(List<ShoppingCartItem> items, UserDetailsDto userDetails);
        List<OrderDto> GetOrders();
        OrderDto ChangeOrderStatus(Guid id,OrderStatus orderStatus);

    }
}
