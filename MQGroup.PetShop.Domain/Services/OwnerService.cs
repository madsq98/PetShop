using System.Collections.Generic;
using System.IO;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;
using MQGroup.PetShop.Domain.Validators;

namespace MQGroup.PetShop.Domain.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _repo;
        private readonly IValidator _validator;

        public OwnerService(IOwnerRepository repo, IValidator validator)
        {
            _repo = repo;
            _validator = validator;
        }
        
        public List<Owner> GetAllOwners()
        {
            return _repo.GetAllOwners();
        }

        public Owner GetOwnerById(int id)
        {
            if (!_validator.OwnerExists(id))
                throw new FileNotFoundException("Owner ID does not exist!");
            
            return _repo.GetOwnerById(id);
        }

        public Owner CreateOwner(Owner owner)
        {
            if (!_validator.ValidateOwner(owner))
                throw new InvalidDataException(_validator.GetErrors());
            
            return _repo.CreateOwner(owner);
        }

        public Owner UpdateOwner(int id, Owner owner)
        {
            if (!_validator.OwnerExists(id))
                throw new FileNotFoundException("Owner ID does not exist!");

            if (!_validator.ValidateOwner(owner))
                throw new InvalidDataException(_validator.GetErrors());
            
            return _repo.UpdateOwner(id, owner);
        }

        public bool DeleteOwnerById(int id)
        {
            if (!_validator.OwnerExists(id))
                throw new FileNotFoundException("Owner ID does not exist!");
            
            return _repo.DeleteOwnerById(id);
        }
    }
}