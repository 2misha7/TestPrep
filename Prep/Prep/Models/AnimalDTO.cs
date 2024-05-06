namespace Prep.Models;

public class AnimalDTO
{
    public Animal animal { get; set; }
    public Owner owner { get; set; }
    public List<Procedure> procedures { get; set; }

}