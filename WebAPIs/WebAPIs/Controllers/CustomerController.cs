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
        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomer([FromQuery] int id)
        {
            Customer customer = await _customerBUS.GetCustomerAsync(id);
            return customer;
        }
        [HttpPost]
        public async Task<ActionResult<int>> PostCustomer(Customer customer)
        {
            try
            {
                int id = await _customerBUS.AddCustomerAsync(customer);
                customer.CustomerId = id;
            }
            catch (Exception){}
            return Ok(1);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> EditCustomer(int id, Customer customer)
        {
            try
            {
                await _customerBUS.EditCustomerAsync(id, customer);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(1);
        }
    }
}
