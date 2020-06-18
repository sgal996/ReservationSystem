using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationSystem.Dto;

namespace ReservationSystem.Views.Home
{
    public class BookModel : PageModel
    {
        
        
        RezDto rez = new RezDto();
        public void OnPost()
        {


            //rez.Create = Request.Form["Datum"];
            //rez.DateBooked = DateTime.Parse(datum);
            //rez.Create = DateBooked;
            rez.Create = DateTime.Now;
            rez.PeopleSum = 5;
            rez.Note = "nista zasad";

            
            
        }
    }
}