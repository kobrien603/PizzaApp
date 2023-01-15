using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PizzaApp.Shared.Models;

namespace PizzaApp.Server.Controllers
{
    public class BaseController : ControllerBase, IActionFilter
    {
        //public AuthUser AuthUser { get; set; } = new();

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string t = "";
            // our code before action executes
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }
}
