using System.ComponentModel.DataAnnotations;

namespace Prep.Models;

public class Owner
{
    public int ID { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
}