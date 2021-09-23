using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.WebApi.DTO;

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
            try
            {
                return Ok(_petService.GetPetById(id));
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public ActionResult<Pet> createPet([FromBody] PetDto p)
        {
            try
            {
                return Ok(_petService.CreatePet(new Pet
                {
                    Name = p.Name,
                    Color = p.Color,
                    Birthdate = p.Birthdate,
                    SoldDate = p.SoldDate,
                    Price = p.Price,
                    Type = new PetType {ID = p.PetTypeId},
                    Owner = new Owner {Id = p.OwnerId}
                }));
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (SystemException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Pet> updatePet(int id, [FromBody] PetDto pet)
        {
            try
            {
                return Ok(_petService.UpdatePet(id, new Pet
                {
                    Name = pet.Name,
                    Color = pet.Color,
                    Birthdate = pet.Birthdate,
                    SoldDate = pet.SoldDate,
                    Price = pet.Price,
                    Type = new PetType {ID = pet.PetTypeId}
                }));
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (SystemException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> deletePet(int id)
        {
            try
            {
                return Ok(_petService.DeletePetById(id));
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (SystemException e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}