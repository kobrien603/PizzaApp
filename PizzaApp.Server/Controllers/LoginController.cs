using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using PizzaApp.Models;
using PizzaApp.Server.DAL;
using PizzaApp.Server.Helpers;
using PizzaApp.Shared.Models;

namespace PizzaApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly PizzaContext _context;

        public LoginController(PizzaContext context)
        {
            _context = context;
        }

        /// <summary>
		/// create new user - store in db
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
        public ValidResponse Login([FromBody] LoginModel model)
        {
            ValidResponse response = new();

            try
            {
                // init repository
                using var repository = new PizzaRepository(_context);

                // get user
                var dbUser = repository.Users.GetUserByEmail(model.Email);
                if (dbUser != null && PasswordHelper.ValidatePassword(model.Password, dbUser.Password))
                {
                    var cookieCreated = new CookieHelper().CreateCookie(dbUser.ID);

                    response.IsValid = cookieCreated.IsValid;
                    response.ResponseMessage = cookieCreated.IsValid ? cookieCreated.Data : cookieCreated.ResponseMessage;
                }
                else
                {
                    response.IsValid = false;
                    response.ResponseMessage = "Login attempt failed. Please try again";
                }

                // todo - add login attempts
                // todo - store IPs
            }
            catch (Exception ex)
            {
                // todo - log
                response.IsValid = false;
                response.ResponseMessage = "Error attempting to login. Please try again later";
            }

            return response;
        }
    }
}
