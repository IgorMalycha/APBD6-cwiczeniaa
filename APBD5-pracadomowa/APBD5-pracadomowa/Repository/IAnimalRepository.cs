using APBD5_pracadomowa.Modules;

namespace APBD5_pracadomowa.Repository;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    void AddAnimal(Animal animal);
    void UpdateAnimal(int id, Animal animal);
    void DeleteAnimal(int id);
}