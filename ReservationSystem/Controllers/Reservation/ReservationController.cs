using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Controllers.Reservation.Impl;
using ReservationSystem.Dto;
using ReservationSystem.Models;

namespace ReservationSystem.Controllers.Reservation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private IReservationHandler _handler;
        private IEmailSender _sender;
       
        public ReservationController(IReservationHandler handler, IEmailSender sender)
        {
            _handler = handler;
            _sender = sender;

        }

        [Route("book")]
        [HttpPost]
        [Authorize]
        public IActionResult Book(RezDto dto)
        {
            //var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            var token = HttpContext.User.Identity as ClaimsIdentity;
            var userId = token.Claims.ToList()[0].Value;
            dto.UserId = userId;

            RezDto newDto = _handler.CreateReservation(dto);
            
            if (newDto == null)
                return BadRequest();

            newDto.UserId = null;
            return Ok(newDto);
        }

        [Route("getrez")]
        [HttpGet]
       // [Authorize(Roles = "admin")]
        public IActionResult GetRezervations()
        {
            List<Rezervation> rezervations = _handler.GetRezervations();

            return Ok(rezervations);
        }
        [HttpPut]
        [Route("Potvrdi")]
        [Authorize(Roles ="admin")]
        public IActionResult Potvrdi([FromBody]PotvrdiDto dto)
        {

            var rezultat=_handler.Potvrdi(dto.ReservationId);
            if (rezultat == false)
                return BadRequest("");
            _sender.SendEmail(dto.Username);

            return Ok();
        }



    }
}