using BusinessLogic.BUS;
using DataAccess.Models.ViewModels;
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
    public class TokenController : ControllerBase
    {
        private readonly IUserBUS _userBUS;
        private readonly ICustomerBUS _customerBUS;
        private readonly IConfiguration _configuration;
        public TokenController(IUserBUS userBUS, IConfiguration configuration, ICustomerBUS customer)
        {
            _userBUS = userBUS;
            _configuration = configuration;
            _customerBUS = customer;
        }
        // POST api/<TokenController>
        [HttpPost, Route("login")]
        public IActionResult Login(ViewLogin viewLogin)
        {
            List<ViewToken> list = new List<ViewToken>();
            if (viewLogin != null && !string.IsNullOrEmpty(viewLogin.UserName) && !string.IsNullOrEmpty(viewLogin.Password))
            {
                var user =  _userBUS.Login(viewLogin);
                if (user != null)
                {
                    if (user != null)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("Id", user.UserId.ToString()),
                            new Claim("FullName", user.FullName),
                            new Claim("UserName", user.UserName)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                            claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                        ViewToken viewToken = new ViewToken()
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            Id = user.UserId,
                            FullName = user.FullName
                        };

                        list.Add(viewToken);
                        //return list;
                        return Ok(new { viewToken.Token, viewToken.FullName });
                    }
                    else
                    {
                        //eturn list;
                        return BadRequest(new { message = "Username or password is correct" });
                    }
                }
                else
                {
                    //return list;
                    return BadRequest();
                }
            }
            //return list;
            return BadRequest();
        } 
        [HttpPost, Route("weblogin")]
        public IActionResult WebLogin(ViewWebLogin viewWebLogin)
        {
            List<ViewToken> list = new List<ViewToken>();
            if (viewWebLogin != null && !string.IsNullOrEmpty(viewWebLogin.Email) && !string.IsNullOrEmpty(viewWebLogin.Password))
            {
                var customer =  _customerBUS.Login(viewWebLogin);
                if (customer != null)
                {
                    if (customer != null)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("Id", customer.CustomerId.ToString()),
                            new Claim("FullName", customer.FullName),
                            new Claim("Email", customer.Email)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                            claims, expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: signIn);

                        ViewToken viewToken = new ViewToken()
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            Id = customer.CustomerId,
                            FullName = customer.FullName
                        };

                        list.Add(viewToken);
                        //return list;
                        return Ok(new { viewToken.Token, viewToken.FullName, viewToken.Id});
                    }
                    else
                    {
                        //eturn list;
                        return BadRequest(new { message = "Username or password is correct" });
                    }
                }
                else
                {
                    //return list;
                    return BadRequest();
                }
            }
            //return list;
            return BadRequest();
        }
    }
}
