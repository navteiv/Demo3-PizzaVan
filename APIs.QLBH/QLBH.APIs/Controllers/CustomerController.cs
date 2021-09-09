using BusinessLogic.BUS;
using DataAccess.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLBH.APIs.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly CustomerBUS _customerBUS;
        public CustomerController()
        {
            var customerBUS = new CustomerBUS();
            _customerBUS = customerBUS;
        }
        // GET: api/Customer
        public IEnumerable<Customer> Get()
        {
            return _customerBUS.GetAllCustomers();
        }

        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
