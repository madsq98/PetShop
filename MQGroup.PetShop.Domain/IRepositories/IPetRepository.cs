using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Domain.IRepositories
{
    public interface IPetRepository
    {
        public List<Pet> ReadAllPets();

        public Pet AddPet(Pet pet);

        public bool DeletePetById(int id);

        public Pet GetPetById(int? id);

        public Pet UpdatePet(int id, Pet pet);

        public List<Pet> ReadPetsByType(PetType petType);
    }
}