using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.BUS
{
    public interface IOrderDetailBUS
    {
        int AddOrderDetailBUS(OrderDetail orderDetail);
    }
    public class OrderDetailBUS : IOrderDetailBUS
    {
        protected DataContext _context;
        public OrderDetailBUS(DataContext context)
        {
            _context = context;
        }
        public int AddOrderDetailBUS(OrderDetail orderDetail)
        {
            int value = 0;
            try
            {
                _context.Add(orderDetail);
                _context.SaveChanges();
                value = orderDetail.DetailId;
            }
            catch (Exception) { value = 0; }
            return value;
        }
    }
}
