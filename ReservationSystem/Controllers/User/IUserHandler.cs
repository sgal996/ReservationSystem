
using Microsoft.AspNetCore.Identity;
using ReservationSystem.Dto;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;


namespace ReservationSystem.Controllers.User
{
    public interface IUserHandler
    {
        List<ApplicationUser> GetUsers();
       RegisterDto Register(RegisterDto dto);
        Object Login(LoginDto dto);
       Object Refresh(string reftoken);

    }
}
