using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Abstractions;
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

        /// <summary>
        /// add user to context on server side when data is found
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var routeData = context.GetRouteData();
            var actionDescriptor = routeData.Values["action"] as ActionDescriptor;
            if (actionDescriptor != null)
            {
                var authorizeData = actionDescriptor.EndpointMetadata.OfType<IAuthorizeData>().FirstOrDefault();
                if (authorizeData != null)
                {
                    var isAuthenticated = await CheckAuthentication(context, authorizeData);
                    if (isAuthenticated)
                    {
                        await next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }
                }
                else
                {
                    await next(context);
                }
            }

            var authHeader = context.Request.Headers["Authorization"];
            if (authHeader.Count > 0)
            {
                try
                {
                    string token = authHeader[0].Replace("Bearer ", "").Trim();
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    
                    context.User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            jwt.Claims, "jwt"
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

        private async Task<bool> CheckAuthentication(HttpContext context, IAuthorizeData authorizeData)
        {
            //Your code to check the authentication of the user based on the authorizeData.
            return true;
        }
    }
}
