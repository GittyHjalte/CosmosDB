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
        
        public async Task<bool> AddSupportMessageAsync(SupportMessage message)
        {
            var response = await _http.PostAsJsonAsync("api/supportmessage/upload", message);
            return response.IsSuccessStatusCode;
        }
        
        public async Task<SupportMessage[]> GetSupportMessagesAsync(string category)
        {
            var url = string.IsNullOrWhiteSpace(category)
                ? "api/supportmessage/support-message"
                : $"api/supportmessage/support-message?category={category}";

            return await _http.GetFromJsonAsync<SupportMessage[]>(url);
        }

    }
}