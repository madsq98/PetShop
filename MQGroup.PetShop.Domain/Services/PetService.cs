using System.Collections.Generic;
using System.Linq;
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

        public Pet UpdatePet(int id, Pet pet)
        {
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