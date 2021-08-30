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
            //POPULATE MOCK
            PetType dog = new PetType{ ID = 1, Name = "Dog" };
            PetType cat = new PetType{ ID = 2, Name = "Cat" };

            Pet nala = new Pet
            {
                Name = "Nala", Color = "Grå", Type = cat, Birthdate = DateTime.Now, SoldDate = DateTime.Now,
                Price = 12.00
            };
            Pet hugo = new Pet
            {
                Name = "Hugo", Color = "Orange", Type = cat, Birthdate = DateTime.Now, SoldDate = DateTime.Now,
                Price = 120.00
            };

            AddPet(nala);
            AddPet(hugo);
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
    }
}