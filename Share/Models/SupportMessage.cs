using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Share.Models
{
    

public class SupportMessage
{
    [JsonProperty("id")]
    public string? Id { get; set; } 
    [JsonProperty("name")]
    public string? Name { get; set; }
    [JsonProperty("email")]
    public string Email { get; set; }
    [JsonProperty("phone")]
    public string Phone { get; set; }
    [JsonProperty("Description")]
    public string Description { get; set; }
    
    [JsonProperty("category")]
    public string Category { get; set; }
    [JsonProperty("date")]
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
}