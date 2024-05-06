using Microsoft.AspNetCore.Mvc;
using Train.Repositories;

namespace Train.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorsRepository _doctorsRepository;

    public DoctorsController(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctor(int id)
    {
        var doctor = await _doctorsRepository.GetDoctor(id);
        return Ok(doctor);
    }
}