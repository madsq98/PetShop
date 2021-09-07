using System;
using System.Collections.Generic;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.Domain.IRepositories;

namespace MQGroup.PetShop.Infrastructure.DataAccess.Repositories
{
    public class PetRepository : IPetRepository
    {
        private static List<Pet> _petTable = new List<Pet>();
        private static int nextID = 1;

        public PetRepository()
        {
        }

        public List<Pet> ReadAllPets()
        {
            return _petTable;
        }

        public Pet AddPet(Pet pet)
        {
            pet.ID = nextID;
            _petTable.Add(pet);

            nextID++;
            return pet;
        }

        public bool DeletePetById(int id)
        {
            Pet p = GetPetById(id);
            if (p != null)
            {
                _petTable.Remove(p);
                return true;
            }

            return false;
        }

        public Pet GetPetById(int? id)
        {
            Pet p = null;
            for (int i = 0; i < _petTable.Count; i++)
            {
                if (_petTable[i].ID == id)
                {
                    p = _petTable[i];
                }
            }

            return p;
        }

        public List<Pet> ReadPetsByType(PetType petType)
        {
            List<Pet> returnList = new List<Pet>();
            foreach (Pet p in _petTable)
            {
                if (p.Type == petType)
                    returnList.Add(p);
            }

            return returnList;
        }
    }
}