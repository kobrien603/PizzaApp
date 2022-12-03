using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PizzaApp.Server.Attributes
{
    /// <summary>
    /// Attribute to set which endpoints need to be authenticated before submitting the request
    /// </summary>
	[AttributeUsage(validOn: AttributeTargets.Class)]
    public class Authenticate : Attribute, IAsyncActionFilter
    {
        /// <summary>
		/// Validate user before calling requested endpoint
		/// </summary>
		/// <param name="actionContext"></param>
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API Key was not provided"
                };
                return;
            }

            //remove Bearer
            extractedApiKey = extractedApiKey.ToString().Replace("Bearer", "").Trim();

            //!new AuthenticationHelper().ValidateToken(token.Replace("Bearer", "").Trim()).IsValid
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = appSettings.GetValue<string>("API_KEY");

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API Key is not valid"
                };
                return;
            }

            await next();
        }
    }
}
