using BusinessLogic.BUS;
using DataAccess.Models;
using DataAccess.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IOrderBUS _orderBUS;
        private readonly IOrderDetailBUS _orderDetailBUS;
        public CartController(IOrderBUS orderBUS, IOrderDetailBUS orderDetailBUS)
        {
            _orderBUS = orderBUS;
            _orderDetailBUS = orderDetailBUS;
        }
        [HttpPost]
        public ActionResult<int> PostCart(Cart cart)
        {

            try
            {
                var order = new Order()
                {
                    OrderStatus = OrderStatus.CurOrder,
                    CusId = cart.CusId,
                    Total = cart.TotalPrice,
                    OrderDate = DateTime.Now,
                    Notes = ""
                };
                int orderId = _orderBUS.AddOrder(order);
                order.OrderId = orderId;

                List<CartItem> dataCart = cart.ListViewCart;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderId = orderId,
                        DishId = dataCart[i].Dish.DishId,
                        Quantity = dataCart[i].Quantity,
                        TotalPrice = dataCart[i].Dish.Price * dataCart[i].Quantity,
                        Notes = ""
                    };
                    _orderDetailBUS.AddOrderDetailBUS(orderDetail);
                }
            }
            catch (Exception)
            {
                return BadRequest(-1);
            }
            return Ok(1);
        }
    }
}
