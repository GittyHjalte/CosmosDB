using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Core.Models;

public class SupportMessage
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    public string Description { get; set; }
    
    public string Category { get; set; }
    
    public DateTime Date { get; set; }
    
}