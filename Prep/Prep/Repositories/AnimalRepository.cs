using System.Data.Common;
using System.Data.SqlClient;
using Prep.Models;
using Prep.Properties;

namespace Prep.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<AnimalDTO?> GetAnimal(int id)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();

        await using var com =
            new SqlCommand("SELECT Name, Type, AdmissionDate, Owner_ID FROM Animal WHERE ID = @IdAnimal", con);
        com.Parameters.AddWithValue("@IdAnimal", id);


        var dr = await com.ExecuteReaderAsync();
        if (!dr.Read()) return null;
        var animal = new AnimalDTO()
        {
            Id = id,
            Name = dr["Name"].ToString(),
            Type = dr["Type"].ToString(),
            AdmissionDate = (DateTime)dr["AdmissionDate"],
            Owner_ID = (int)dr["Owner_ID"]
        };

        await dr.CloseAsync();
        com.Parameters.Clear();
        com.CommandText = "SELECT ID, FirstName, LastName FROM Owner WHERE ID = @IdOwner";
        com.Parameters.AddWithValue("@IdOwner", animal.Owner_ID);
        var dr1 = await com.ExecuteReaderAsync();
        if (!dr1.Read()) return null;

        animal.owner = new Owner()
        {
            ID = animal.Owner_ID,
            FirstName = dr1["FirstName"].ToString(),
            LastName = dr1["LastName"].ToString()
        };

        return animal;
    }

    public async Task<int> AddAnimal(PostDTO postDto)
{
    using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
    await con.OpenAsync();
    using var tran = con.BeginTransaction();
    try
    {
        var procedures = postDto.Procedures;
        var Name = postDto.Name;
        var Type = postDto.Type;
        var date = postDto.AdmissionDate;
        var owner = postDto.Owner_ID;

        // Вставляем новое животное в таблицу Animal
        using (var com = new SqlCommand("INSERT INTO ANIMAL(Name, Type, AdmissionDate, Owner_ID) VALUES (@Name, @Type, @AdmissionDate, @Owner_ID); SELECT SCOPE_IDENTITY();", con, tran))
        {
            com.Parameters.AddWithValue("@Name", Name);
            com.Parameters.AddWithValue("@Type", Type);
            com.Parameters.AddWithValue("@AdmissionDate", date);
            com.Parameters.AddWithValue("@Owner_ID", owner);
            var id = Convert.ToInt32(await com.ExecuteScalarAsync());

            com.Parameters.Clear(); 

            
            foreach (var pr in procedures)
            {
                using (var comProc = new SqlCommand("INSERT INTO Procedure_Animal(Procedure_ID, Animal_ID, Date) VALUES (@Procedure_ID, @Animal_ID, @Date);", con, tran))
                {
                    comProc.Parameters.AddWithValue("@Procedure_ID", pr.ProcedureId);
                    comProc.Parameters.AddWithValue("@Animal_ID", id);
                    comProc.Parameters.AddWithValue("@Date", pr.Date);
                    await comProc.ExecuteNonQueryAsync();
                    comProc.Parameters.Clear(); 
                }
            }

            await tran.CommitAsync();
            return id; 
        }
    }
    catch (SqlException e)
    {
        await tran.RollbackAsync();
        throw; // Пробрасываем исключение дальше
    }
}


}