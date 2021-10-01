using BusinessLogic.BUS;
using BusinessLogic.Helpers;
using DataAccess.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IUploadHelper _uploadHelper;
        private readonly IDishBUS _dishBUS;
        public DishController(IDishBUS dishBUS, IWebHostEnvironment webHostEnvironment, IUploadHelper uploadHelper)
        {
            _dishBUS = dishBUS;
            _webHostEnvironment = webHostEnvironment;
            _uploadHelper = uploadHelper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Dish>> GetDish()
        {
            return _dishBUS.GetAllDishes();
        }
        [HttpPost]
        public ActionResult PostDish(Dish dish)
        {
            try
            {
                if (dish.ImageFile != null)
                {
                    if (dish.ImageFile.Length > 0)
                    {
                        string rootPath = Path.Combine(_webHostEnvironment.WebRootPath, "assets/images");
                        _uploadHelper.UploadImage(dish.ImageFile, rootPath, "Dish");
                        dish.Image = dish.ImageFile.FileName;
                    }
                }
                _dishBUS.AddDish(dish);
                return Ok(1);
            }
            catch (Exception)
            { return BadRequest(); }
        }
    }
}
