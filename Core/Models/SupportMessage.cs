using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Core.Models;

public class SupportMessage
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    public string Phone { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string Category { get; set; }
    
    public DateTime Date { get; set; }
    
}