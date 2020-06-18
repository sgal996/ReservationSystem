using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReservationSystem.Data;
using ReservationSystem.Dto;
using ReservationSystem.Models;

namespace ReservationSystem.Controllers.Reservation.Impl
{
    public class ReservationHandler : IReservationHandler
    {
       
        ApplicationDbContext _context;
        
       

        public ReservationHandler(ApplicationDbContext context)
        {

            _context = context;
            
                        
        }

        public RezDto CreateReservation(RezDto dto)
        {
          
            var rez = new Rezervation();
           
            rez.Create = dto.Create;
            rez.PeopleSum = dto.PeopleSum;
            rez.Note = dto.Note;
            
            rez.ApplicationUserId = dto.UserId;
            
            
            
            try
            {
                _context.Rezervations.Add(rez);
                _context.SaveChanges();
            }
            catch
            {
                return null;
            }
            

            return dto;

        }

        public List<Rezervation> GetRezervations()
        {
            List<Rezervation> vratirez = new List<Rezervation>();            

            var rezzIZbaze = _context.Rezervations.Where(o => o.Create > DateTime.Now && o.Confirmed==false ).Include(o=>o.ApplicationUser);           

            foreach (var temp in rezzIZbaze)
                vratirez.Add(temp);

            return vratirez;
            
        }

        public bool Potvrdi(int rezId)
        {
            var rezz = _context.Rezervations.FirstOrDefault(o=>o.Id==rezId);
            if (rezz == null)
                return false;

            rezz.Confirmed = true;
            try
            {
                _context.Rezervations.Update(rezz);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
