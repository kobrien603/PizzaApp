using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.Services
{
    public class CookieService
    {
        private readonly IJSRuntime _jsRuntime;

        public CookieService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetCookie(string name, string value, int days)
        {
            await _jsRuntime.InvokeVoidAsync("setCookie", name, value, days);
        }

        public async Task<string> GetCookie(string name)
        {
            return await _jsRuntime.InvokeAsync<string>("getCookie", name);
        }

        public async Task DeleteCookie(string name)
        {
            await _jsRuntime.InvokeVoidAsync("deleteCookie", name);
        }
    }
}
