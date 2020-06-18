using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace ReservationSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string RefreshToken { get; set; }
    }
}
