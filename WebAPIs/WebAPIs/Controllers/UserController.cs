using BusinessLogic.BUS;
using DataAccess.Models;
using DataAccess.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBUS _userBUS;
        private readonly IConfiguration _configuration;
        public UserController(IUserBUS userBUS, IConfiguration configuration)
        {
            _userBUS = userBUS;
            _configuration = configuration;
        }
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            var user =  _userBUS.GetAllUsers();
            return user;
        }
        [HttpGet("{id}")]
        public ActionResult<User> GetUser([FromQuery] int id)
        {
            User user = _userBUS.GetUser(id);
            return user;
        }
        [HttpPost]
        public ActionResult<int> PostUser(User user)
        {
            try
            {
                int id = _userBUS.AddUser(user);
                user.UserId = id;
            }
            catch (Exception){}
            return Ok(1);
        }
        [HttpPut("{id}")]
        public ActionResult<int> PutUser(int id, User user)
        {
            try
            {
                _userBUS.EditUser(id, user);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return Ok(1);
        }
        [HttpDelete("{id}")]
        public ActionResult<int> DeleteUser(int id)
        {
            try
            {
                _userBUS.DeleteUser(id);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(1);
        }
    }
}
