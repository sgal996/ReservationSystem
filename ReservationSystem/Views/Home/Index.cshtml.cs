using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationSystem.Dto;

namespace ReservationSystem.Views.Home
{
    public class IndexModel : PageModel
    {

        RegisterDto dto = new RegisterDto();
        

        public void OnGet()
        {

            //dto.Email = Request.Form["Email"];
            //dto.Password = Request.Form["Password"];
            //dto.Username = Request.Form["Username"];


        }
    }
}