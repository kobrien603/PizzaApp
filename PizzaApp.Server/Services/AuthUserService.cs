using PizzaApp.Server.DAL;
using PizzaApp.Shared.Models;
using System.Security.Claims;

namespace PizzaApp.Server.Services
{
    public class AuthUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PizzaContext _context;

        public AuthUserService(IHttpContextAccessor httpContextAccessor, PizzaContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Username()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name) ?? "";
            }
            return result;
        }

        public async Task<AuthUser> GetUser()
        {
            AuthUser user = new();
            if (_httpContextAccessor.HttpContext != null)
            {
                if (int.TryParse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int userID))
                {
                    // init repository
                    using var repository = new PizzaRepository(_context);
                    var dbUser = await repository.Users.GetUserByID(userID);
                    if (dbUser != null)
                    {
                        user = new()
                        {
                            CreatedDate = dbUser.CreatedDate,
                            DateOfBirth = dbUser.DateOfBirth.HasValue ? dbUser.DateOfBirth.Value.ToDateTime(new TimeOnly()) : null,
                            Email = dbUser.Email,
                            FirstName = dbUser.FirstName,
                            ID = dbUser.ID,
                            LastName = dbUser.LastName,
                            ModifiedDate = dbUser.ModifiedDate,
                            PhoneNumber = dbUser.PhoneNumber,
                            ProfilePicture = dbUser.ProfilePicture,
                        };
                    }
                }
            }

            return user;
        }
    }
}
