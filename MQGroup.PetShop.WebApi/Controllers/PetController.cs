using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public ActionResult<List<Pet>> getAllPets()
        {
            return Ok(_petService.GetAllPets());
        }

        [HttpGet("{id}")]
        public ActionResult<Pet> getPetById(int id)
        {
            return Ok(_petService.GetPetById(id));
        }

        [HttpPost]
        public ActionResult<Pet> createPet([FromBody] Pet p)
        {
            return Ok(_petService.CreatePet(p));
        }

        [HttpPut("{id}")]
        public ActionResult<Pet> updatePet(int id, [FromBody] Pet pet)
        {
            if (pet.ID != id)
            {
                return BadRequest("ID from path does not match Pet ID in body.");
            }

            return Ok(pet);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> deletePet(int id)
        {
            return Ok(_petService.DeletePetById(id));
        }
    }
}