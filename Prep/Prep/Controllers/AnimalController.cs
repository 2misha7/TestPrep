using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Prep.Models;
using Prep.Services;

namespace Prep.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalController : ControllerBase
{
    public readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAnimal(int id)
    {
        try
        {
            var animalDto = await _animalService.GetAnimal(id);
            return Ok(animalDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}