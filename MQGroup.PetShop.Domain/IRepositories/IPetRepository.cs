using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Domain.IRepositories
{
    public interface IPetRepository
    {
        public List<Pet> ReadAllPets();

        public Pet AddPet(Pet pet);
    }
}