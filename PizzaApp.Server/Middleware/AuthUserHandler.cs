using Microsoft.AspNetCore.Authorization;
using PizzaApp.Shared.Models;

namespace PizzaApp.Server.Middleware
{
    public class AuthUserHandler : AuthorizationHandler<IAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            //// Fill the user model with the user's information
            //var userId = context.User.FindFirst("sub")?.Value;
            //var userName = context.User.Identity.Name;
            //var userRoles = context.User.FindAll("role").Select(c => c.Value);
            //var userModel = new AuthUser { ID = userId, Name = userName, Roles = userRoles };
            //context.HttpContext.Items["UserModel"] = userModel;
            //context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
