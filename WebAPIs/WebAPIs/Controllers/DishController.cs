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
    public class DishController : ControllerBase
    {
        private readonly IDishBUS _dishBUS;
        public DishController(IDishBUS dishBUS)
        {
            _dishBUS = dishBUS;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Dish>> GetDish()
        {
            return _dishBUS.GetAllDishes();
        }
    }
}
