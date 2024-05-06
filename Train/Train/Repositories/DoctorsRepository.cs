using System.Data.SqlClient;
using System.Runtime.InteropServices.JavaScript;
using Train.Models.DTOs;

namespace Train.Repositories;

public class DoctorsRepository : IDoctorsRepository
{
    private IConfiguration _configuration;
    private IDoctorsRepository _doctorsRepositoryImplementation;

    public DoctorsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<DoctorDTO> GetDoctor(int id)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        await using var com = new SqlCommand("SELECT FirstName, LastName, Email FROM Doctor WHERE IdDoctor = @IdDoctor", con);
        com.Parameters.AddWithValue("@IdDoctor", id);
        var reader = await com.ExecuteReaderAsync();
        if (!reader.Read()) return null;
        DoctorDTO doctor = new DoctorDTO()
        {
            IdDoctor = id,
            FirstName = reader["FirstName"].ToString(),
            LastName = reader["LastName"].ToString(),
            Email = reader["Email"].ToString(),
            Prescriptions = new List<Prescription>()
        };
        
        await reader.CloseAsync();
        com.CommandText = "SELECT IdPrescription, Date, DueDate, IdPatient FROM Prescription WHERE IdDoctor = @IdDoctor ORDER BY Date DESC";
        var reader1 = await com.ExecuteReaderAsync();
        while (reader1.Read())
        {
            
            Prescription prescription = new Prescription()
            {
                IdPrescription = (int)reader1["IdPrescription"],
                Date = (DateTime)reader1["Date"],
                DueDate = (DateTime)reader1["DueDate"],
                IdPatient = (int)reader1["IdPatient"]
            };
            doctor.Prescriptions.Add(prescription);
        }
        await reader1.CloseAsync();
        return doctor;
    }

    public async Task<int> DeleteDoctor(int id)
    {
        int affectedRows = 0;
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        await using var com = new SqlCommand("", con);
        var tran = await con.BeginTransactionAsync();
        com.Transaction = (SqlTransaction)tran;
        try
        {
            // Delete from Prescription_Medicament
            com.CommandText = "DELETE pm FROM Prescription_Medicament pm " +
                              "JOIN Prescription p ON p.IdPrescription = pm.IdPrescription " +
                              "WHERE p.IdDoctor = @IdDoctor";
            com.Parameters.AddWithValue("@IdDoctor", id);
            affectedRows += await com.ExecuteNonQueryAsync();

            // Delete from Prescription
            com.CommandText = "DELETE FROM Prescription WHERE IdDoctor = @IdDoctor";
            affectedRows += await com.ExecuteNonQueryAsync();

            // Delete from Doctor
            com.CommandText = "DELETE FROM Doctor WHERE IdDoctor = @IdDoctor";
            affectedRows += await com.ExecuteNonQueryAsync();
            await tran.CommitAsync();
        }
        catch (Exception)
        {
            await tran.RollbackAsync();
            throw;
        }
        return affectedRows;
    }
}