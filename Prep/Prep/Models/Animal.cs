using System.ComponentModel.DataAnnotations;

namespace Prep.Models;

public class Animal
{
    public int ID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public DateTime AdmissionDate { get; set; }
    [Required]
    public int Owner_ID { get; set; }
    
}

