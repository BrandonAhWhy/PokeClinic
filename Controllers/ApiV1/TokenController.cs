using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PokeClinic.Models
{

    class BearerTokenFilter : ActionFilterAttribute
    {
        private StringValues _token;

        public bool authAdmin{get ; set ;}
        public bool authID{get; set;}


        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            try{
                int userRole = -1;
                int userId = -1;

                //get request ID to validate
                int reqID = int.Parse(actionContext.RouteData.Values["id"].ToString());
                //get Auth token and trim
                var authHeader = actionContext.HttpContext.Request.Headers.TryGetValue("Authorization", out _token);
                if (!authHeader)
                    throw(new Exception("Failed to get Authorization headers from request"));
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
                if (authAdmin && (int)USER_ROLE.ADMIN != userRole){
                    actionContext.Result =  new UnauthorizedResult();

                }
                //check if user ID is valid
                if (!authAdmin && authID && userId != reqID) { 
                    actionContext.Result =  new UnauthorizedResult();
                }

            }
            catch (Exception err)
            {
                ExceptionHandler handler = new HandleArgumentException(ref actionContext);
                handler.RegisterNext(new TokenExpiredException(ref actionContext));
                handler.HandleException(err);

            }
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
                throw new Exception("ClaimsPrincipal is [null]: failed to parse claims");
            }
            catch(Exception err){
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