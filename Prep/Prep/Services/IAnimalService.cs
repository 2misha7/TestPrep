using Prep.Models;

namespace Prep.Services;

public interface IAnimalService
{
    public Task<AnimalDTO> GetAnimal(int id);
}