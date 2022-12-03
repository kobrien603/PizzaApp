using PizzaApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Server.Helpers
{
    public class CookieHelper
    {
        public ValidResponse<string> CreateCookie(int userID)
        {
            ValidResponse<string> response = new();
            try
            {
                response.IsValid = true;
                response.Data = EncryptionHelper.EncryptString($"{userID};{DateTime.Today.AddHours(24)}");
                response.ResponseMessage = "Successful";
            }
            catch(Exception ex)
            {
                // todo - log
                response.IsValid = false;
                response.ResponseMessage = "Error generating cookies";
            }

            return response;
        }
    }
}
