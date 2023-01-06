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
        public ValidResponse CreateCookie(int userID)
        {
            ValidResponse response = new();
            try
            {
                response.IsValid = true;
                response.ResponseMessage = EncryptionHelper.EncryptString($"{userID};{DateTime.Today.AddHours(24)}");
            }
            catch(Exception e)
            {
                // todo - log
                response.IsValid = false;
                response.ResponseMessage = "Error generating cookies";
            }

            return response;
        }
    }
}
