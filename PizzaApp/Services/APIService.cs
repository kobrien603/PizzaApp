using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PizzaApp.Services
{
    public class APIService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public APIService(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async Task<T> Get<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                var statusCode = response.EnsureSuccessStatusCode();

                if (typeof(T).IsClass || typeof(T).IsValueType)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions()
                    {
                        AllowTrailingCommas = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });
                }
                else
                {
                    return (T)(object)await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
            }

            return default;
        }

        public async Task<HttpResponseMessage> Post<T>(string url, T data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
            }

            return default;
        }

        public async Task<HttpResponseMessage> Put<T>(string url, T data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
            }

            return default;
        }

        public async Task<HttpResponseMessage> Delete(string url)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
            }

            return default;
        }
    }
}
