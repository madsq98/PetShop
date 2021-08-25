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
    }
}