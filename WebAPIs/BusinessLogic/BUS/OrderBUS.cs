using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.BUS
{
    public interface IOrderBUS
    {
        List<Order> GetAllOrders();
        List<Order> GetOrderByCustomer(int customerId);
        Order GetOrder(int id);
        int AddOrder(Order order);
        int EditOrder(int id, Order order);
    }
    public class OrderBUS : IOrderBUS
    {
        protected DataContext _context;
        public OrderBUS(DataContext context)
        {
            _context = context;
        }
        public List<Order> GetAllOrders()
        {
            List<Order> list = new List<Order>();
            //list = _context.Orders.OrderByDescending(x => x.OrderDate)
            //    .Include(x => x.Customer)
            //    .Include(x => x.OrderDetails)
            //    .ToList();
            list = _context.Orders.OrderByDescending(x => x.OrderDate).ToList();
            return list;
        }
        public List<Order> GetOrderByCustomer(int customerId)
        {
            List<Order> list = new List<Order>();
            list = _context.Orders.Where(x => x.CusId == customerId).OrderByDescending(x => x.OrderDate)
                //.Include(x => x.Customer)
                //.Include(x => x.OrderDetails)
                .ToList();
            return list;
        }
        public Order GetOrder(int id)
        {
            Order order = null;
            order = _context.Orders.Where(x => x.OrderId == id)
                .Include(x => x.Customer)
                .Include(x => x.OrderDetails).ThenInclude(y => y.Dish)
                .FirstOrDefault();
            return order;
        }
        public int AddOrder(Order order)
        {
            int value = 0;
            try
            {
                _context.Add(order);
                _context.SaveChanges();
                value = order.OrderId;
            }
            catch (Exception) { value = 0; }
            return value;
        }
        public int EditOrder(int id, Order order)
        {
            int value = 0;
            try
            {
                //Dish _dish = null;
                //_dish = _context.Dishes.Find(id);

                //_dish.Name = dish.Name;
                //_dish.Price = dish.Price;
                //_dish.Category = dish.Category;
                //_dish.Image = dish.Image;
                //_dish.Description = dish.Description;
                //_dish.Status = dish.Status;

                //_context.Update(_dish);
                //_context.SaveChanges();
                //value = dish.DishId;
                Order _order = null;
                _order = _context.Orders.Find(id);

                _order.OrderStatus = order.OrderStatus;
                _order.Notes = order.Notes;

                _context.Update(_order);
                _context.SaveChanges();
                value = order.OrderId;
            }
            catch (Exception) { value = 0; }
            return value;
        }
    }
}
