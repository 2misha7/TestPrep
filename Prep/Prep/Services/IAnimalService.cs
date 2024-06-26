﻿using Prep.Models;

namespace Prep.Services;

public interface IAnimalService
{
    Task<AnimalDTO?> GetAnimal(int id);
    Task<int> AddAnimal(PostDTO postDto);
}