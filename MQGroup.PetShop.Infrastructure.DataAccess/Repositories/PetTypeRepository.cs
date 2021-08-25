using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;

namespace MQGroup.PetShop.Infrastructure.DataAccess.Repositories
{
    public class PetTypeRepository : IPetTypeRepository
    {
        private static List<PetType> _petTypeTable = new List<PetType>();

        public PetTypeRepository()
        {
            //POPULATE MOCK
        }
    }
}