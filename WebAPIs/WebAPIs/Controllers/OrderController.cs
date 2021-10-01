using BusinessLogic.BUS;
using DataAccess.Models;
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
    public class OrderController : ControllerBase
    {
        private IOrderBUS _orderBUS;
        public OrderController(IOrderBUS orderBUS)
        {
            _orderBUS = orderBUS;
        }
        [HttpGet("{id}")]
        public ActionResult<List<Order>> GetOrder(int id)
        {
            var order = _orderBUS.GetOrderByCustomer(id);
            return order;
        }
        [HttpGet]
        public ActionResult<List<Order>> GetOrder()
        {
            var list = _orderBUS.GetAllOrders();
            return list;
        }
    }
}
