using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Shared.Models;

namespace PizzaApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("get-user")]
        public async Task<ValidResponse<AuthUser>> GetAuthUser()
        {
            return new ValidResponse<AuthUser>();
        }
    }
}
