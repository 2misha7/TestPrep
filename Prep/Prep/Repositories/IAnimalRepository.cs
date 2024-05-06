using Prep.Models;

namespace Prep.Repositories;

public interface IAnimalRepository
{
    Task<AnimalDTO?> GetAnimal(int id);
    Task<int> AddAnimal(PostDTO postDto);
}