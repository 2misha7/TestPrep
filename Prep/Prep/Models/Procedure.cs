using System.ComponentModel.DataAnnotations;

namespace Prep.Models;

public class Procedure
{
    public int ID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}