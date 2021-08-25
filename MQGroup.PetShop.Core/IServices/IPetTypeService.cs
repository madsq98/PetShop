using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.Core.IServices
{
    public interface IPetTypeService
    {
        public List<PetType> GetAllPetTypes();

        public PetType GetByID(int id);
    }
}