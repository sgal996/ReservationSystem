using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class Rezervation
    {
        
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime Create { get; set; }
        public int PeopleSum { get; set; }
        public string Note { get; set; }
        public bool Confirmed { get; set; }
    }
}
