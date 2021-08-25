using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Domain.IRepositories
{
    public interface IPetTypeRepository
    {
        public List<PetType> GetAllPetTypes();

        public PetType GetByID(int id);
    }
}