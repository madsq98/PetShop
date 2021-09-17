using System.Collections.Generic;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;

namespace MQGroup.PetShop.Domain.Services
{
    public class PetTypeService : IPetTypeService
    {
        private IPetTypeRepository _repo;

        public PetTypeService(IPetTypeRepository repo)
        {
            _repo = repo;
        }

        public List<PetType> GetAllPetTypes()
        {
            return _repo.GetAllPetTypes();
        }

        public PetType GetByID(int id)
        {
            return _repo.GetByID(id);
        }

        public PetType SavePetType(PetType petType)
        {
            return _repo.SavePetType(petType);
        }

        public bool DeletePetTypeById(int id)
        {
            return _repo.DeletePetTypeById(id);
        }

        public PetType UpdatePetType(int id, PetType petType)
        {
            return _repo.UpdatePetType(id, petType);
        }
    }
}