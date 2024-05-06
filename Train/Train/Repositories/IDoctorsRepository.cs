using Train.Models.DTOs;

namespace Train.Repositories;

public interface IDoctorsRepository
{
    Task<DoctorDTO> GetDoctor(int id);
    Task<int> DeleteDoctor(int id);
}