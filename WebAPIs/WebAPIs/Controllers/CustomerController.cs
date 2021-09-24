using BusinessLogic.BUS;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBUS _customerBUS;
        public CustomerController(ICustomerBUS customerBUS)
        {
            _customerBUS = customerBUS;
        }
        [HttpGet("{id}")]
        public  ActionResult<Customer> GetCustomer([FromQuery] int id)
        {
            Customer customer = _customerBUS.GetCustomer(id);
            return customer;
        }
        [HttpGet]
        public ActionResult<List<Customer>> GetCustomer()
        {
            var list = _customerBUS.GetAllCustomers();
            return list;
        }
        [HttpPost]
        public ActionResult<int> PostCustomer(Customer customer)
        {
            try
            {
                int id =  _customerBUS.AddCustomer(customer);
                customer.CustomerId = id;
            }
            catch (Exception){}
            return Ok(1);
        }
        [HttpPut("{id}")]
        public ActionResult<int> EditCustomer(int id, Customer customer)
        {
            try
            {
                 _customerBUS.EditCustomer(id, customer);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(1);
        }
    }
}
