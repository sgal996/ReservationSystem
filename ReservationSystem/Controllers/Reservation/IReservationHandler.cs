using ReservationSystem.Dto;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Controllers.Reservation
{
    public interface IReservationHandler
    {

        RezDto CreateReservation(RezDto dto);
        List<Rezervation> GetRezervations();
        bool Potvrdi(int rezId);


    }
}
