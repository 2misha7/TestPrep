using System.ComponentModel.DataAnnotations;

namespace Train.Models.DTOs;

public class DoctorDTO
{
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Email { get; set; }

    public List<Prescription> Prescriptions { get; set; }
}

public class Prescription
{
    [Required]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public int IdPatient { get; set; }
}