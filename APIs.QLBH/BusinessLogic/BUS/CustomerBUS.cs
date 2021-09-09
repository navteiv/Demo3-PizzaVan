using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EF;
using PagedList;

namespace BusinessLogic.BUS
{
    public class CustomerBUS
    {
        readonly DataContext _context = null;
        public CustomerBUS()
        {
            _context = new DataContext();
        }
        public IEnumerable<Customer> GetAllCustomers(string search, int page, int pageSize)
        {
            IEnumerable<Customer> model = _context.Customers;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.FullName.ToUpper().Contains(search.ToUpper()) || x.Email.ToString().ToUpper().Contains(search.ToUpper()));
            }
            return model.OrderBy(x => x.CustomerId).ToPagedList(page, pageSize);
        }
        public Customer GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }
        public int AddCustomer(Customer customer)
        {
            int value = 0;
            try
            {
                var duplicatedCustomer = _context.Customers.Count(x => x.Email == customer.Email);
                if (duplicatedCustomer > 0)
                {
                    return value;
                }
                _context.Customers.Add(customer);
                _context.SaveChanges();
                value = customer.CustomerId;
            }
            catch (Exception)
            {
                value = 0;
            }
            return value;
        }
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                var _customer = _context.Customers.Find(customer.CustomerId);
                _customer.FullName = customer.FullName;
                _customer.DoB = customer.DoB;
                _customer.Email = customer.Email;
                _customer.PhoneNumber = customer.PhoneNumber;
                _customer.Address = customer.Address;
                if (!string.IsNullOrEmpty(customer.Password))
                {
                    _customer.Password = customer.Password;
                    _customer.ConfirmPassword = customer.Password;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Customer GetCustomer(string email)
        {
            return _context.Customers.SingleOrDefault(x => x.Email == email);
        }
        public bool Login(string userName, string passWord)
        {
            var result = _context.Customers.Count(x => x.Email == userName && x.Password == passWord);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
