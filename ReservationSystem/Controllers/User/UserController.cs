using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Dto;
using ReservationSystem.Models;


namespace ReservationSystem.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        //private IHttpContextAccessor _httpContextAccessor;
        private IUserHandler _handler;
        public UserController( IUserHandler handler)
        {
            _handler = handler;
           // _httpContextAccessor = httpContextAccessor;
        }


        
        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginDto dto) 
        {


            var token = _handler.Login(dto);
            if (token == null)
                return BadRequest("Prijava nije uspjela");
            
            return Ok(token);
        }

        [Route("refreshToken/{refreshToken}")]
        [HttpGet]
        public IActionResult refreshToken([FromRoute] string refreshToken) 
        {
            var token = _handler.Refresh(refreshToken);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }


        [Route("register")]
        [HttpPost]
        public IActionResult Register(RegisterDto dto)
        {
            

            RegisterDto newDto = _handler.Register(dto);
            if (newDto == null)
            {
                return BadRequest();
            }
            return Ok(newDto);
        }

        //[Route("GetUsers")]
        //[HttpGet]
        //[Authorize (Roles ="admin")]
        public IActionResult GetUsers()
        {
            List<ApplicationUser> users = _handler.GetUsers();
            

            return Ok(users);
        }
    }
}