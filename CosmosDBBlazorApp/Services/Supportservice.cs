using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Share.Models;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace CosmosDBBlazorApp.Services
{
    public class SupportMessageService
    {
        private readonly HttpClient _http;

        public SupportMessageService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Adds a support message via the API.
        /// </summary>
        public async Task<bool> AddSupportMessageAsync(SupportMessage message)
        {
            var response = await _http.PostAsJsonAsync("api/supportmessage/upload", message);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Optional: get support messages by category.
        /// </summary>
        public async Task<SupportMessage[]> GetSupportMessagesAsync(string category)
        {
            return await _http.GetFromJsonAsync<SupportMessage[]>($"supportmessage?category={category}");
        }

    }
}