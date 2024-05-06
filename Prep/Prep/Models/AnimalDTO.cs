using System.ComponentModel.DataAnnotations;

namespace Prep.Models;

public class AnimalDTO
{
    
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Type { get; set; }
    [Required]
    public DateTime AdmissionDate { get; set; }
    [Required]
    public int Owner_ID { get; set; }
    
    public Owner owner { get; set; }
    public List<Procedure> procedures { get; set; }

}