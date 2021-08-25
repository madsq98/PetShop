using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;

namespace MQGroup.PetShop.Infrastructure.DataAccess.Repositories
{
    public class PetRepository : IPetRepository
    {
        private static List<Pet> _petTable = new List<Pet>();

        public PetRepository()
        {
            //POPULATE MOCK
        }

        public List<Pet> ReadAllPets()
        {
            return _petTable;
        }
    }
}