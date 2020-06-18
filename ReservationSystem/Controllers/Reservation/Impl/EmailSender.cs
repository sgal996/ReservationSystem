using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using ReservationSystem.Data;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ReservationSystem.Controllers.Reservation.Impl
{
    public class EmailSender : IEmailSender
    {
        public ApplicationDbContext _contex ;
        public EmailSender(ApplicationDbContext context)
        {
            _contex = context;
        }


        public void SendEmail(string username)
        {
            var email = _contex.ApplicationUsers.FirstOrDefault(o => o.UserName == username).Email;

            var fromAddress = new MailAddress("galacsim@gmail.com", "");
            var toAddress = new MailAddress(email, "");
            const string fromPassword = "nikojenemozepogoditi";
            const string subject = "Što ćemo raditi";
            const string body = "Što kažeš na ovo??";

            var smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
