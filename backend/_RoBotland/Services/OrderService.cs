using _RoBotland.Helpers;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using _RoBotland.Enums;
using _RoBotland.Migrations;
using Microsoft.IdentityModel.Tokens;

namespace _RoBotland.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public OrderService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public OrderDto ChangeOrderStatus(Guid id, OrderStatus orderStatus)
        {
            var order=_dataContext.Orders.Find(id)?? throw new Exception();
            _dataContext.Entry(order).Entity.OrderStatus = orderStatus;
            _dataContext.SaveChanges();
            order.OrderStatus = orderStatus;
            return _mapper.Map<OrderDto>(order);
        }

        public List<OrderDto> GetOrders()
        {
            var orders = _dataContext.Orders.ToList();
            List<OrderDto> orderList = new List<OrderDto>();
            orders.ForEach(x =>
            {
                if (x != null)
                {
                    var orderDto = _mapper.Map<OrderDto>(x);
                    var userD = _dataContext.UserDetails.FirstOrDefault(p => p.Id == x.UserDetailsId) ?? throw new Exception("User not found");
                    var orderDetails = _dataContext.OrderDetails.Where(d => d.OrderId == x.Id).ToList();
                    int i = 1;
                    orderDto.Items = new List<ShoppingCartItem>();
                    orderDetails.ForEach(d => {
                        if (d != null)
                        {
                            var product = _mapper.Map<ProductDto>(_dataContext.Products.Find(d.ProductId)) ?? throw new Exception("");
                            ShoppingCartItem item = new ShoppingCartItem(i++, product, d.Quantity, d.Total);
                            orderDto.Items.Add(item);
                        }
                    });
                    orderDto.UserDetails = _mapper.Map<UserDetailsDto>(userD);
                    orderList.Add(orderDto);
                }
            });
            return orderList;
        }

        public OrderDto PlaceOrderByLoggedInUser(ISession session,string username,OrderOptionsDto orderOptions)
        {
            if (username is null) throw new Exception("unlogged user");
            float total = 0;
            var items = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart") ?? throw new Exception("Empty card");
            var user = _dataContext.Users.FirstOrDefault(x => x.Username == username) ?? throw new Exception("User not found");
            var userD = _dataContext.UserDetails.FirstOrDefault(x => x.Id == user.Id) ?? throw new Exception("User not found");
            var order = new Order(Guid.NewGuid(), userD.Id, userD, total, Enums.OrderStatus.A, orderOptions.DeliveryType, orderOptions.PaymentType);
            _dataContext.Orders.Add(order);
            items.ForEach(x => {
                total += x.Total;
                var orderDetail = _mapper.Map<OrderDetails>(x);
                var product = _dataContext.Products.Find(x.Product.Id) ?? throw new Exception("Product not found");
                orderDetail.OrderId = order.Id;
                orderDetail.Product = product;
                orderDetail.Order = order;
                _dataContext.OrderDetails.Add(orderDetail);
            });
            order.Total = total;
            if (user.AccountBalance >= total)
            {
                _dataContext.Entry(user).Entity.AccountBalance -= total;
            }
            else
            {
                throw new Exception("lack of account funds\r\n");
            }
            _dataContext.SaveChanges();
            OrderDto orderDto = new OrderDto(order.Id,DateTime.Now,items,_mapper.Map<UserDetailsDto>(userD),orderOptions.DeliveryType,orderOptions.PaymentType);
            return orderDto;
        }

        public OrderDto PlaceOrderWithoutRegister(ISession session, UserDetailsDto userDetails)
        {
            var items = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart") ?? throw new Exception("Empty card");
            float total = 0;
            var orderId = Guid.NewGuid();
            var userDId = Guid.NewGuid();
            var userD =_mapper.Map<UserDetails>(userDetails);
            userD.User = new Models.User();
            userD.User.Id = userDId;
            userD.User.Username = "";
            userD.User.PasswordHash = string.Empty;
            userD.Id = userDId;
            _dataContext.UserDetails.Add(userD);
            _dataContext.SaveChanges();
            var order = new Order(orderId, userDId, userD, total, Enums.OrderStatus.A, userDetails.DeliveryType,userDetails.PaymentType);
            _dataContext.Orders.Add(order);
            items.ForEach(x => {
                total += x.Total;
                var orderDetail = _mapper.Map<OrderDetails>(x);
                orderDetail.OrderId = orderId;
                orderDetail.Product = _mapper.Map<Product>(x.Product);
                orderDetail.Order = order;
                orderDetail.ProductId = x.Product.Id;
                _dataContext.OrderDetails.Add(orderDetail);
            });
            order.Total = total;
            OrderDto orderDto = new OrderDto(orderId, DateTime.Now, items, _mapper.Map<UserDetailsDto>(userD), userDetails.DeliveryType, userDetails.PaymentType);
            return orderDto;
        }
    }
}
