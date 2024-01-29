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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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
            var order=_dataContext.Orders.First(x=>x.Id==id)?? throw new Exception();
            _dataContext.Entry(order).Entity.OrderStatus = orderStatus;
            _dataContext.SaveChanges();
            order.OrderStatus = orderStatus;
            return _mapper.Map<OrderDto>(order);
        }

        public List<OrderDto> GetOrders()
        {
            var orders = _dataContext.Orders.
                Include(x=>x.UserDetails)
                .Include(x=> x.OrderDetails).
                ThenInclude(x=>x.Product).
                ToList();
            List<OrderDto> orderList =new List<OrderDto>();
            orders.ForEach(x =>
            {
                var orderDto = _mapper.Map<OrderDto>(x);
                orderDto.Items = new List<ShoppingCartItem>();
                x.OrderDetails.ToList().ForEach(y =>
                {
                    orderDto.Items.Add(new ShoppingCartItem(orderDto.Items.Count, _mapper.Map<ProductDto>(y.Product), y.Quantity, y.Total));
                });
                orderDto.UserDetails = _mapper.Map<UserDetailsDto>(x.UserDetails);
                orderList.Add(orderDto);
            });
            return orderList;
        }

        public OrderDto PlaceOrderByLoggedInUser(List <ShoppingCartItem> items,string username,OrderOptionsDto orderOptions)
        {
            if (username is null) throw new Exception("unlogged user");
            if (items.IsNullOrEmpty()) throw new Exception("empty cart");
            float total = 0;
            var user = _dataContext.Users.Include(x=>x.UserDetails).FirstOrDefault(x => x.Username == username) ?? throw new Exception("User not found");
            var order = new Order(Guid.NewGuid(), user.UserDetails.Id, user.UserDetails, total, Enums.OrderStatus.A, orderOptions.DeliveryType, orderOptions.PaymentType);
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
                _dataContext.Entry(user).Entity.AccountBalance -= total;
            else
                throw new Exception("lack of account funds\r\n");
            _dataContext.SaveChanges();
            OrderDto orderDto = new OrderDto(order.Id,DateTime.Now,items,_mapper.Map<UserDetailsDto>(user.UserDetails),orderOptions.DeliveryType,orderOptions.PaymentType,Enums.OrderStatus.A);
            return orderDto;
        }

        public OrderDto PlaceOrderWithoutRegister(List<ShoppingCartItem> items, UserDetailsDto userDetails)
        {
            float total = 0;
            var orderId = Guid.NewGuid();
            var userDId = Guid.NewGuid();
            if (items.IsNullOrEmpty()) throw new Exception("empty cart");
            var userD =_mapper.Map<UserDetails>(userDetails);
            userD.User = new Models.User(userDId, "", "");
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
            OrderDto orderDto = new OrderDto(orderId, DateTime.Now, items, _mapper.Map<UserDetailsDto>(userD), userDetails.DeliveryType, userDetails.PaymentType, Enums.OrderStatus.A);
            return orderDto;
        }
    }
}
