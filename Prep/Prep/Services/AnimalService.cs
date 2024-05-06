using Prep.Models;
using Prep.Properties;
using Prep.Repositories;

namespace Prep.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public async Task<AnimalDTO?> GetAnimal(int id)
    {
        return await _animalRepository.GetAnimal(id);
    }

    public async Task<int> AddAnimal(PostDTO postDto)
    {
        return await _animalRepository.AddAnimal(postDto);
    }
}