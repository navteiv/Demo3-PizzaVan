using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BusinessLogic.BUS
{
    public class OrderBUS
    {
        readonly DataContext _context = null;
        public OrderBUS()
        {
            _context = new DataContext();
        }
        public IEnumerable<Order> GetAllOders()
        {
            return _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderDetails)
                .OrderByDescending(x => x.OrderDate).ToList();
        }
        public List<Order> GetOrderByCustomer(int customerId)
        {
            List<Order> list = new List<Order>();
            list = _context.Orders.Where(x => x.CusId == customerId).OrderByDescending(x => x.OrderDate)
                .Include(x => x.Customer).Include(x => x.OrderDetails).ToList();
            return list;
        }
        public Order GetOrderById(int id)
        {
            return _context.Orders.Where(x => x.OrderId == id)
                .Include(x => x.Customer)
                .Include(x => x.OrderDetails).FirstOrDefault();
        }
    }
}
