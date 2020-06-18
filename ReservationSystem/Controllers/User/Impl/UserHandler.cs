using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ReservationSystem.Data;
using ReservationSystem.Dto;
using ReservationSystem.Models;

namespace ReservationSystem.Controllers.User.Impl
{
    public class UserHandler : IUserHandler
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;
        

        public UserHandler(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public List<ApplicationUser> GetUsers()
        {
            List<ApplicationUser> vratiusere = new List<ApplicationUser>();
            var users = _context.ApplicationUsers;
            var linq = from user in users
                       where user.Email !=null
                       select user;

            foreach (var temp in linq)
                vratiusere.Add(temp);

            return vratiusere;
          
                
        }

        public object Login(LoginDto dto)  
        {
           
            

            
            var userTmp = new ApplicationUser();
            var user =_context.ApplicationUsers.FirstOrDefault(u=>u.UserName==dto.Username );
            

            if (user == null)
            {
                return null;
            }

            if (_userManager.PasswordHasher.VerifyHashedPassword(user,user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
            {

                return null;
            }

            bool isAdmin=false;

            var rolTmpe = _context.UserRoles.FirstOrDefault(o => o.UserId == user.Id);
            if (rolTmpe != null)
            {
                isAdmin = true;
            }


            
            var claims = new List<Claim>();
            IdentityUserRole<string> userRoles = new IdentityUserRole<string>();
            userRoles = _context.UserRoles.FirstOrDefault(o => o.UserId == user.Id);
            if (userRoles != null)
            {
                var role = _context.Roles.FirstOrDefault(o => o.Id == userRoles.RoleId);
                claims.Add(new Claim(ClaimTypes.Role, role.Name)); //admin
            }
            
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));//user
            


            var siningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurityKey"));
            var refreshToken = RandomStringRefreshToken();
            user.RefreshToken = refreshToken;
            _context.ApplicationUsers.Update(user);
            _context.SaveChanges();

           

            var token = new JwtSecurityToken(
                issuer: "http://oec.com",
                audience: "http://oec.com",
                expires: DateTime.UtcNow.AddMinutes(30),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(siningKey, SecurityAlgorithms.HmacSha256)
                );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = user.RefreshToken,
                isAdmin
                
            };
            
        }

        
        private string RandomStringRefreshToken()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public RegisterDto Register(RegisterDto dto)
        {



            
            
            var user = new ApplicationUser();
            
            user.UserName = dto.Username;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);
            user.Email = dto.Email;

            try
            {
                _context.ApplicationUsers.Add(user);

                _context.SaveChanges();
            }

            catch
            {
                return null;
            }

            
            

            


            dto.Password = String.Empty;

            return dto;
        }

        

        object IUserHandler.Refresh(string reftoken)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(o => o.RefreshToken == reftoken);
            if (user == null)
                return null;
            user.RefreshToken = RandomStringRefreshToken();
            _context.ApplicationUsers.Update(user);
            _context.SaveChanges();

            var claims = new List<Claim>();
            IdentityUserRole<string> userRoles = new IdentityUserRole<string>();
            userRoles = _context.UserRoles.FirstOrDefault(o => o.UserId == user.Id);
            if (userRoles != null)
            {
                var role = _context.Roles.FirstOrDefault(o => o.Id == userRoles.RoleId);
                claims.Add(new Claim(ClaimTypes.Role, role.Name)); //admin
            }

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));//user


            var siningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurityKey"));


            // UpdateUserRefreshToken(user.Id, refreshToken);

            var token = new JwtSecurityToken(
                issuer: "http://oec.com",
                audience: "http://oec.com",
                expires: DateTime.UtcNow.AddMinutes(30),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(siningKey, SecurityAlgorithms.HmacSha256)
                );

            KeyValuePair<string, JwtSecurityToken> value = new KeyValuePair<string, JwtSecurityToken>(user.RefreshToken, token);
            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(value.Value),
                refreshToken = user.RefreshToken
            };
        }

        
    }
}
