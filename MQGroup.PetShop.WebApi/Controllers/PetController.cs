﻿using System.Collections.Generic;
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
            return Ok(_petService.GetPetById(id));
        }

        [HttpPost]
        public ActionResult<Pet> createPet([FromBody] PetDto p)
        {
            return Ok(_petService.CreatePet(new Pet
            {
                Name = p.Name,
                Color = p.Color,
                Birthdate = p.Birthdate,
                SoldDate = p.SoldDate,
                Price = p.Price,
                Type = new PetType { ID = p.PetTypeId },
                Owner = new Owner { Id = p.OwnerId }
            }));
        }

        [HttpPut("{id}")]
        public ActionResult<Pet> updatePet(int id, [FromBody] PetDto pet)
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

        [HttpDelete("{id}")]
        public ActionResult<bool> deletePet(int id)
        {
            return Ok(_petService.DeletePetById(id));
        }
    }
}