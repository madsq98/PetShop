using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Core.IServices
{
    public interface IPetService
    {
        public List<Pet> GetAllPets();

        public Pet CreatePet(Pet pet);

        public bool DeletePetById(int id);

        public Pet GetPetById(int id);
    }
}