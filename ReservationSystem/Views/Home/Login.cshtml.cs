using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationSystem.Controllers.User;
using ReservationSystem.Dto;

namespace ReservationSystem.Views.User
{
    public class LoginModel : PageModel
    {

        LoginDto dto = new LoginDto();

        public void OnPost()
        {
           
            dto.Password = Request.Form["Password"];
            dto.Username = Request.Form["Username"];

            var accessToken = Request.Headers["Authorization"];

        }

        public void OnGet()
        {
            

        }

        
    }
   
}