using System.ComponentModel.DataAnnotations;

namespace Prep.Models;

public class Owner
{ 
    [Required]
    public int ID { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
}