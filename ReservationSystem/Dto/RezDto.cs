using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Dto
{
    public class RezDto
    {
        public DateTime Create { get; set; }
        public int PeopleSum { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
       
    }
}
