using System.Data.SqlClient;
using Train.Models.DTOs;

namespace Train.Repositories;

public class DoctorsRepository : IDoctorsRepository
{
    private IConfiguration _configuration;

    public DoctorsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<DoctorDTO> GetDoctor(int id)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        await using var com = new SqlCommand("SELECT FirstName, LastName, Email FROM Doctor WHERE ID = @IdDoctor", con);
        com.Parameters.AddWithValue("@IdDoctor", id);
        var reader = await com.ExecuteReaderAsync();
        if (!reader.Read()) return null;
        var doctor = new DoctorDTO()
        {
            IdDoctor = id,
            FirstName = reader["FirstName"].ToString(),
            LastName = reader["LastName"].ToString(),
            Email = reader["Email"].ToString()
        };
        

    }
}