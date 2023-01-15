using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using PizzaApp.Server.DAL;
using PizzaApp.Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace PizzaApp.Server.Middleware
{
    public class CustomAuthMiddleware : IMiddleware
    {
        private readonly IConfiguration _configuration;

        public CustomAuthMiddleware(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var authHeader = context.Request.Headers["Authorization"];
            if (authHeader.Count > 0)
            {
                try
                {
                    string token = authHeader[0].Replace("Bearer ", "").Trim();
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    
                    context.User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            jwt.Claims, "custom"
                        )
                    );
                }
                catch (Exception e)
                {
                    string ts = "";
                }
                // Validate the authHeader value
                //if (authHeader[0] == _configuration.GetSection("JwtConfig:secret").Value)
                //{
                //    // Set the authenticated user on the context
                //    context.User = new ClaimsPrincipal(
                //        new ClaimsIdentity(
                //            new[] { 
                //                new Claim(ClaimTypes.Name, "authenticated-user") 
                //            }, "custom"
                //        )
                //    );
                //}
                //else
                //{
                //    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //    return;
                //}
            }
            //else
            //{
            //    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //    return;
            //}

            await next(context);
        }
    }
}
