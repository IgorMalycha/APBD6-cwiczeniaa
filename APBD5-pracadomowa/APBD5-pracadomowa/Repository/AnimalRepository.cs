using System.Data.SqlClient;
using APBD5_pracadomowa.Modules;

namespace APBD5_pracadomowa.Repository;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        
        command.CommandText = "SELECT * FROM Animals;";

        var reader = command.ExecuteReader();

        var animals = new List<Animal>();

        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameOrdinal = reader.GetOrdinal("Name");
        int descOrdinal = reader.GetOrdinal("Description");
        int categoryOrdinal = reader.GetOrdinal("Category");
        int areaOrdinal = reader.GetOrdinal("Area");

        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameOrdinal),
                Description = reader.GetString(descOrdinal),
                Category = reader.GetString(categoryOrdinal),
                Area = reader.GetString(areaOrdinal)
            });
        }

        if (orderBy == null || orderBy == "Name")
        {
            animals.OrderBy(animal => animal.Name);
        }else if (orderBy == "Description")
        {
            animals.OrderBy(animal => animal.Description);
        }else if (orderBy == "Area")
        {
            animals.OrderBy(animal => animal.Area);
        }else if (orderBy == "Category")
        {
            animals.OrderBy(animal => animal.Category);
        }

        return animals;
    }
    public void AddAnimal(Animal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES (@animalName, @animalDescritpion, @animalCategory, @animalArea);";
        command.Parameters.AddWithValue("animalName", animal.Name);
        command.Parameters.AddWithValue("animalDescritpion", animal.Description);
        command.Parameters.AddWithValue("animalCategory", animal.Category);
        command.Parameters.AddWithValue("animalArea", animal.Area);

        command.ExecuteNonQuery();
    }

    public void UpdateAnimal(int id, Animal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE Animals SET Name = @animalName, " +
                              "Description = @animalDescritpion, Category = @animalCategory, Area = @animalArea";
        command.Parameters.AddWithValue("animalName", animal.Name);
        command.Parameters.AddWithValue("animalDescritpion", animal.Description);
        command.Parameters.AddWithValue("animalCategory", animal.Category);
        command.Parameters.AddWithValue("animalArea", animal.Area);

        command.ExecuteNonQuery();

    }

    public void DeleteAnimal(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM Animals WHERE Id = @animalId";
        command.Parameters.AddWithValue("animalId", id);

        command.ExecuteNonQuery();
    }
    
    
}