using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;
using MQGroup.PetShop.Domain.Validators;

namespace MQGroup.PetShop.Domain.Services
{
    public class PetService : IPetService
    {
        private IPetRepository _repo;
        private IValidator _validator;

        public PetService(IPetRepository repo, IValidator validator)
        {
            _repo = repo;
            _validator = validator;
        }
        
        public List<Pet> GetAllPets()
        {
            return _repo.ReadAllPets();
        }

        public Pet CreatePet(Pet pet)
        {
            if (!_validator.ValidatePet(pet))
                throw new InvalidDataException(_validator.GetErrors());
            
            return _repo.AddPet(pet);
        }

        public bool DeletePetById(int id)
        {
            if (!_validator.PetExists(id))
                throw new FileNotFoundException("Pet ID does not exist!");
            return _repo.DeletePetById(id);
        }

        public Pet GetPetById(int id)
        {
            if (!_validator.PetExists(id))
                throw new FileNotFoundException("Pet ID does not exist!");
            return _repo.GetPetById(id);
        }

        public Pet UpdatePet(int id, Pet pet)
        {
            if (!_validator.PetExists(id))
                throw new FileNotFoundException("Pet ID does not exist!");
                
            if (!_validator.ValidatePet(pet))
                throw new InvalidDataException(_validator.GetErrors());
            return _repo.UpdatePet(id, pet);
        }

        public List<Pet> GetPetsByType(PetType petType)
        {
            return _repo.ReadPetsByType(petType);
        }

        public List<Pet> SortPetsByPrice(List<Pet> sortList)
        {
            return sortList.OrderBy(o => o.Price).ToList();
        }
    }
}