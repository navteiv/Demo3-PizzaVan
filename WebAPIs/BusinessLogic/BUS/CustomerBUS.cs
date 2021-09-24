using BusinessLogic.Helpers;
using DataAccess.Models;
using DataAccess.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BUS
{
    public interface ICustomerBUS
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomer(int id);
        int AddCustomer(Customer customer);
        int EditCustomer(int id, Customer customer);
        Customer Login(ViewWebLogin viewweblogin);
    }
    public class CustomerBUS : ICustomerBUS
    {
        protected DataContext _context;
        protected IEncryptHelper _encryptHelper;
        public CustomerBUS(IEncryptHelper encrypt, DataContext context)
        {
            _context = context;
            _encryptHelper = encrypt;
        }
        public List<Customer> GetAllCustomers()
        {
            List<Customer> list = new List<Customer>();
            list = _context.Customers.ToList();
            return list;
        }
        public Customer GetCustomer(int id)
        {
            Customer customer = null;
            customer = _context.Customers.Find(id);
            return customer;
        }

        public Customer Login(ViewWebLogin viewweblogin)
        {
            var u = _context.Customers.Where(
                p => p.Email.Equals(viewweblogin.Email)
                && p.Password.Equals(_encryptHelper.MD5Encrypt(viewweblogin.Password)))
                .FirstOrDefault();
            return u;
        }
        public int AddCustomer(Customer customer)
        {
            int value = 0;
            try
            {
                customer.Password = _encryptHelper.MD5Encrypt(customer.Password);
                customer.ConfirmPassword = customer.Password;

                _context.Add(customer);
                _context.SaveChanges();
                value = customer.CustomerId;
            }
            catch (Exception) { value = 0; }
            return value;
        }


        public int EditCustomer(int id, Customer customer)
        {
            int value = 0;
            try
            {
                Customer _customer = null;
                _customer = _context.Customers.Find(id);

                _customer.FullName = customer.FullName;
                _customer.DoB = customer.DoB;
                _customer.PhoneNumber = customer.PhoneNumber;
                _customer.Email = customer.Email;
                if (customer.Password != null)
                {
                    customer.Password = _encryptHelper.MD5Encrypt(customer.Password);
                    _customer.Password = customer.Password;
                    _customer.ConfirmPassword = customer.Password;
                }
                _customer.Address = customer.Address;

                _context.Update(_customer);
                _context.SaveChanges();
            }
            catch (Exception) { value = 0; }
            return value;
        }
    }
}
