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

    public async Task<AnimalDTO> GetAnimal(int id)
    {
        await using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await using var com = new SqlCommand("SELECT Name, Type, AdmissionDate, Owner_ID FROM Animal WHERE ID = @IDanimal");
        com.Parameters.AddWithValue("@IDanimal", id);
        await con.OpenAsync();
        DbTransaction tran = await con.BeginTransactionAsync();
        com.Transaction = (SqlTransaction)tran;
        
        var dr = com.ExecuteReader();
        if (!dr.Read()) return null;
        var animal = new AnimalDTO()
        {
            animal = new Animal()
            {
                ID = (int)dr["ID"],
                Name = dr["Name"].ToString(),
                Type = dr["Type"].ToString(),
                AdmissionDate = (DateTime)dr["AdmissionDate"],
                Owner_ID = (int)dr["Owner_ID"]
            }
        };
        return animal;








    }
}