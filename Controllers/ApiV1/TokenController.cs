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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PokeClinic.Models
{

    internal class BearerTokenFilter : ActionFilterAttribute
    {
        private StringValues _token;

        public string Role{get ; set ;}
        public bool CheckUserID{get; set;}


        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            try{
                int _Role = 0;
                int userRole = -1;
                int userId = -1;
                if (Role == "ADMIN")
                    _Role = (int)USER_ROLE.ADMIN;
                //get request ID to validate
                int reqID = int.Parse(actionContext.RouteData.Values["id"].ToString());
                //get Auth token and trim
                var authHeader = actionContext.HttpContext.Request.Headers.TryGetValue("Authorization", out _token);
                string token = _token[0].Replace("Bearer","").Trim();
                ClaimsPrincipal principal = TokenController.ValidateToken(token);
                //mapping claims from decoded jwt
                foreach (Claim claim in principal.Claims)  
                {
                    if (claim.Type == "Role"){
                        userRole = int.Parse(claim.Value);
                    }
                    if(claim.Type == "Id"){
                        userId = int.Parse(claim.Value);
                    }
                } 
                //check if role is valid
                if ((int)USER_ROLE.ADMIN == _Role &&  (int)USER_ROLE.ADMIN != userRole){
                    actionContext.Result =  new UnauthorizedResult();
                }
                //check if user ID is valid 
                if (CheckUserID && userId != reqID){
                    actionContext.Result =  new UnauthorizedResult();
                }

            }catch(Exception err){
                //return 500  with exception message here!(failed to parse token)
                actionContext.Result =  new UnauthorizedResult();
            }
        }
    }
    public class ErrorDetails
    {
        public ErrorDetails(int _statusCode, string _Message)
        {
            StatusCode = _statusCode;
            Message = _Message;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class TokenController
    {

        public static JwtBearerOptions JwtOptions{get ; set ;}
        
        TokenController()
        {
            JwtOptions = new JwtBearerOptions();
            
            JwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PokeDB.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        public static ClaimsPrincipal ValidateToken(string base_64_token)
        {
            try
            {
                //this will throw an error if it cannot parse the token
                SecurityToken _jwt = new JwtSecurityToken(base_64_token);

                var handler = new JwtSecurityTokenHandler();
                TokenValidationParameters _validation = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PokeDB.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                ClaimsPrincipal claims = handler.ValidateToken(base_64_token, _validation, out _jwt);
                if (claims != null)
                {
                    return claims;
                }
                return null;
            }
            catch (Exception err)
            {
                Console.Write("ValidateToken Exception Thrown : " + err.Message);
                throw err;
            }
        }
        public static string GenToken(User _user)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(PokeDB.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Id", _user.Id.ToString()),
                        new Claim("Email", _user.Email),
                        new Claim("Name", _user.Name),
                        new Claim("Role", _user.Role.ToString())
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
    }
}