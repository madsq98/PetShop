using System.Collections.Generic;
using System.IO;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;
using MQGroup.PetShop.Domain.Validators;

namespace MQGroup.PetShop.Domain.Services
{
    public class PetTypeService : IPetTypeService
    {
        private readonly IPetTypeRepository _repo;
        private readonly IValidator _validator;

        public PetTypeService(IPetTypeRepository repo, IValidator validator)
        {
            _repo = repo;
            _validator = validator;
        }

        public List<PetType> GetAllPetTypes()
        {
            return _repo.GetAllPetTypes();
        }

        public PetType GetByID(int id)
        {
            if (!_validator.PetTypeExists(id))
                throw new FileNotFoundException("Pet Type ID does not exist!");
            
            return _repo.GetByID(id);
        }

        public PetType SavePetType(PetType petType)
        {
            if (!_validator.ValidatePetType(petType))
                throw new InvalidDataException(_validator.GetErrors());
            
            return _repo.SavePetType(petType);
        }

        public bool DeletePetTypeById(int id)
        {
            if (!_validator.PetTypeExists(id))
                throw new FileNotFoundException("Pet Type ID does not exist!");
            
            return _repo.DeletePetTypeById(id);
        }

        public PetType UpdatePetType(int id, PetType petType)
        {
            if (!_validator.PetTypeExists(id))
                throw new FileNotFoundException("Pet Type ID does not exist!");

            if (!_validator.ValidatePetType(petType))
                throw new InvalidDataException(_validator.GetErrors());
            
            return _repo.UpdatePetType(id, petType);
        }
    }
}