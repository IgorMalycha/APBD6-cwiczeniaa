using APBD5_pracadomowa.Modules;
using APBD5_pracadomowa.Repository;
using Microsoft.AspNetCore.Mvc;

namespace APBD5_pracadomowa.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    [HttpGet]
    public IActionResult GetAnimals([FromQuery] string orderBy)
    {
        var list = _animalRepository.GetAnimals(orderBy);

        return Ok(list);
    }

    [HttpPost]
    public IActionResult PostAnimal([FromBody] Animal animal)
    {

        _animalRepository.AddAnimal(animal);
        
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("id:int")]
    public IActionResult PutAnimal(int id, Animal animal)
    {

        _animalRepository.UpdateAnimal(id, animal);
        return NoContent();
    }

    [HttpDelete("id:int")]
    public IActionResult DeleteAnimal(int id)
    {
        _animalRepository.DeleteAnimal(id);
        
        return NoContent();
    }


}