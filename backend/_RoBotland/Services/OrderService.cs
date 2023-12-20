using _RoBotland.Helpers;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using _RoBotland.Enums;
using _RoBotland.Migrations;

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

        public OrderDto PlaceOrderByLoggedInUser(ISession session,string username,OrderOptionsDto orderOptions)
        { 
            if (username == null) throw new Exception("unlogged user");
            var orderId = Guid.NewGuid();
            float total = 0;
            var items = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart") ?? throw new Exception("Empty card");
            var user = _dataContext.Users.FirstOrDefault(x => x.Username == username) ?? throw new Exception("User not found");
            var userD = _dataContext.UserDetails.FirstOrDefault(x => x.Id == user.Id) ?? throw new Exception("User not found");
            var order = new Order(orderId, userD.Id, userD, total, Enums.OrderStatus.A, orderOptions.DeliveryType, orderOptions.PaymentType);
            _dataContext.Orders.Add(order);
            items.ForEach(x => { 
                total += x.Total;
                var orderDetail=_mapper.Map<OrderDetails>(x);
                orderDetail.OrderId = orderId;
                orderDetail.Product = _mapper.Map<Product>(x.Product);
                orderDetail.Order = order;
                orderDetail.ProductId = x.Product.Id;
                _dataContext.OrderDetails.Add(orderDetail); 
            });
            order.Total = total;
            if (user.AccountBalance >= total)
            {
                _dataContext.Entry(user).Entity.AccountBalance -= total;
                //if do sprawdzenia stanu konta
            }
            else
            {
                throw new Exception("lack of account funds\r\n");
            }
            _dataContext.SaveChanges();
            OrderDto orderDto = new OrderDto(orderId,DateTime.Now,items,userD,orderOptions.DeliveryType,orderOptions.PaymentType);
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
            OrderDto orderDto = new OrderDto(orderId, DateTime.Now, items, userD, userDetails.DeliveryType, userDetails.PaymentType);
            return orderDto;
        }
    }
}
