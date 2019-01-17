using Film.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Film.Controllers
{
    public static class Account
    {
        

        public static Tuple<string,DateTime> BuildToken(User userInfo)
        {  
             

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("secret_key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expiration = DateTime.UtcNow.AddDays(7);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);
               string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Tuple.Create(tokenString,expiration);
             

        }
    }
}
