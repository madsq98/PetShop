using System.Collections.Generic;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;

namespace MQGroup.PetShop.Domain.Services
{
    public class PetService : IPetService
    {
        private IPetRepository _repo;

        public PetService(IPetRepository repo)
        {
            _repo = repo;
        }
        
        public List<Pet> GetAllPets()
        {
            return _repo.ReadAllPets();
        }

        public Pet CreatePet(Pet pet)
        {
            return _repo.AddPet(pet);
        }

        public bool DeletePetById(int id)
        {
            return _repo.DeletePetById(id);
        }

        public Pet GetPetById(int id)
        {
            return _repo.GetPetById(id);
        }
    }
}