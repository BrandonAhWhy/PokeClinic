using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PokeClinic.Models;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace PokeClinic.Models
{

    public class TokenController
    {
        public static bool ValidateToken(string base_64_token)
        {
            //this will throw an error if it cannot parse the token
            SecurityToken _jwt = new JwtSecurityToken(base_64_token);

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(PokeDB.Secret);
            TokenValidationParameters _validation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            if (handler.ValidateToken(base_64_token, _validation, out _jwt) != null)
            {
                return true;
            }
            return false;

        }
        public static string GenToken(string email, string username)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(PokeDB.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, email),
                        new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = handler.CreateToken(tokenDescriptor);
                return handler.WriteToken(token);
            }
            catch
            {
                Console.WriteLine("Failed To Gen Token");
                return null;
            }

        }

        public static Task PreTokenParser(ref MessageReceivedContext context)
        {
            try
            {
                HttpRequest req = context.Request;
                var _keys = (IList<string>)req.Headers.Keys;
                var _values = (IList<StringValues>)req.Headers.Values;
                int index = _keys.IndexOf("Authorization");
                if (index >= 0)
                {
                    string token = _values[index].ToString().Substring(7);
                    bool test3 = TokenController.ValidateToken(token);
                    return Task.CompletedTask;
                }
                return Task.FromException(new Exception("Auth token missing from header"));
            }
            catch (Exception err)
            {
                HttpResponse _resp = context.HttpContext.Response;
                // unsure of how to write a response for bad tokens just setting headers here
                context.Fail(err.Message);
                _resp.StatusCode = 401;
                return Task.CompletedTask;
            }
        }
    }
}