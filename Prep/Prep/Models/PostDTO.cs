using System.ComponentModel.DataAnnotations;

namespace Prep.Models;

public class PostDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public DateTime AdmissionDate { get; set; }
    [Required]
    public int Owner_ID { get; set; }
    [Required]
    public List<ProcedureWithDate> Procedures { get; set; }
    
}

public class ProcedureWithDate
{
    public int ProcedureId { get; set; }
    public DateTime Date { get; set; }
}