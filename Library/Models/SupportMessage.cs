using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Core.Models;

public class SupportMessage
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    
    public string name { get; set; }
    
    public string email { get; set; }
    
    public string phone { get; set; }
    
    public string description { get; set; }
    
    public string category { get; set; }
    
    public DateTime date { get; set; }
    
}